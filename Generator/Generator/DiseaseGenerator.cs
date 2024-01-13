using Bogus;
using Generator.Models;
using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Linq;

namespace Generator.Generator
{
    internal class DiseaseGenerator
    {
        static List<Diseases> disieses = new List<Diseases>();
        Random r = new Random();
        public static List<Diseases> Generate(int count)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://normativ.kontur.ru/document?moduleId=1&documentId=437250");
            var disease_names = Parse(doc);

            var diseaseRules = new Faker<Diseases>()
               .StrictMode(true)
               .RuleFor(v => v.id, f => Guid.NewGuid())
               .RuleFor(v => v.disease_name, f =>
               {
                   string name = f.PickRandom(disease_names);
                   while (Contains(name))
                       name = f.PickRandom(disease_names);
                   return name;
               });
            for (int i = 0; i < count; ++i)
                disieses.Add(diseaseRules.Generate());
            return disieses;
        }

        private static bool Contains(string name)
        {
            foreach (var disease in disieses)
                if (disease.disease_name == name)
                    return true;

            return false;
        }

        private static List<string> Parse(HtmlDocument doc)
        {
            List<string> list = new List<string>();
            var tables = doc.DocumentNode.SelectNodes("//table");

            foreach (var table in tables)
            {
                foreach (HtmlNode row in table.SelectNodes("tr").Skip(1))
                {
                    HtmlNodeCollection cells = row.SelectNodes("td");
                    if (cells != null)
                        if (cells.Count > 2)
                            list.Add(cells[2].InnerText);

                }
            }
            return list;
        }
    }
}
