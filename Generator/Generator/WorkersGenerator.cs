using Bogus;
using Generator.Models;
using System;
using System.Collections.Generic;

namespace Generator.Generator
{
    internal class WorkersGenerator
    {
        static List<Workers> workers = new List<Workers>();
        Random r = new Random();
        public static List<Workers> Generate(int count, List<Positions> positions)
        {
            var workerRules = new Faker<Workers>()
               .StrictMode(true)
               .RuleFor(v => v.id, f => Guid.NewGuid())
               .RuleFor(v => v.fullname, f => f.Name.FullName())
               .RuleFor(v => v.position_id, f => f.PickRandom(positions).id)
               .RuleFor(v => v.login, f => f.Name.LastName())
               .RuleFor(v => v.salt, f => StringGenerators.Generate(32))
               .RuleFor(v => v.hash_password, f => StringGenerators.Generate(34));

            for (int i = 0; i < count; ++i)
                workers.Add(workerRules.Generate());
            return workers;
        }
    }
}
