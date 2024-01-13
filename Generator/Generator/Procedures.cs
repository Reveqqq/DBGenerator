using Bogus;
using Generator.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Generator.Generator
{
    internal class ProceduresGenerator
    {
        static List<Procedures> procedures = new List<Procedures>();
        Random r = new Random();

        public static List<Procedures> Generate(int count)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://aurora-altai.ru/therapy/procedures/");
            var procedures_names = Parse(doc);

            var procedureRules = new Faker<Procedures>()
               .StrictMode(true)
               .RuleFor(v => v.id, f => Guid.NewGuid())
               .RuleFor(v => v.procedure_title, f => {
                   string title = f.PickRandom(procedures_names);
                   while (Contains(title))
                       title = f.PickRandom(procedures_names);
                   return title;
               });

            for (int i = 0; i < count; ++i)
                procedures.Add(procedureRules.Generate());
            return procedures;
        }

        private static bool Contains(string title)
        {
            foreach (var procedure in procedures)
                if (procedure.procedure_title == title)
                    return true;

            return false;
        }

        private static List<string> Parse(HtmlDocument doc)
        {
            List<string> list = new List<string>();
            var nodes = doc.DocumentNode.SelectNodes(".//div[@class='col-12 col-md-6']");

            foreach (var node in nodes)
            {
                foreach (HtmlNode ul in node.SelectNodes("ul"))
                {
                    HtmlNodeCollection li = ul.SelectNodes("li");
                    if (li != null)
                        foreach (var el in li)
                            list.Add(el.InnerText);
                }

            }
            return list;
        }
    }
}