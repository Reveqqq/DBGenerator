using Bogus;
using Generator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Generator.Generator
{
    internal class PositionsGenerator
    {

        static List<Positions> positions = new List<Positions>();
        Random r = new Random();

        public static List<Positions> Generate(int count)
        {
            var url = "https://pandia.ru/text/78/628/63360.php";
            HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding("windows-1251");
            HtmlDocument doc = web.Load(url);
            var position_names = Parse(doc);

            var positionRules = new Faker<Positions>()
               .StrictMode(true)
               .RuleFor(v => v.id, f => Guid.NewGuid())
               .RuleFor(v => v.position_title, f =>
               {
                   string name = f.PickRandom(position_names[f.Random.Number(0, 5)].Take(9));
                   while (Contains(name))
                       name = f.PickRandom(position_names[f.Random.Number(0, 5)].Take(9));
                   return name;
               });

            for (int i = 0; i < count; ++i)
                positions.Add(positionRules.Generate());
            return positions;
        }

        private static bool Contains(string name)
        {
            foreach (var position in positions)
                if (position.position_title == name)
                    return true;

            return false;
        }

        private static Dictionary<int, List<string>> Parse(HtmlDocument doc)
        {
            int i = 0;
            Dictionary<int, List<string>> result = new Dictionary<int, List<string>>();
            var tables = doc.DocumentNode.SelectNodes(".//table");
            if (tables != null && tables.Count > 0)
            {
                foreach (var table in tables)
                {
                    var rows = table.SelectNodes(".//tr");
                    if (rows != null && rows.Count > 0)
                    {
                        var res = new List<string>();

                        foreach (var row in rows)
                        {
                            var cells = row.SelectNodes(".//td");
                            if (cells != null && cells.Count > 0)
                            {
                                var a = cells.Select(c => c.InnerText).ToArray();
                                if (a.Length >= 2)
                                    res.Add(a[1].Trim());
                            }
                        }
                        if (res.Count > 1)
                            result[i++] = res.Skip(1).ToList();
                    }
                }
                return result;
            }
            else
            {
                Console.WriteLine("No tables");
                return null;
            }
        }
    }
}
