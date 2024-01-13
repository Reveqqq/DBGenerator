using Bogus;
using Generator.Models;
using System;
using System.Collections.Generic;

namespace Generator.Generator
{
    internal class AssignedProceduresGenerator
    {
        static List<AssignedProcedures> assignedProcedures = new List<AssignedProcedures>();
        Random r = new Random();
        public static List<AssignedProcedures> Generate(List<Vacationers> vacationers, List<Procedures> procedures)
        {
            foreach (var vacationer in vacationers)
            {
                var diagnosesRules = new Faker<AssignedProcedures>()
               .StrictMode(true)
               .RuleFor(v => v.snn, f => vacationer.snn)
               .RuleFor(v => v.skk_code, f => vacationer.skk_code)
               .RuleFor(v => v.procedure_id, f => f.PickRandom(procedures).id)
               .RuleFor(v => v.n_assigned, f => short.Parse(f.Random.Number(5, 10).ToString()))
               .RuleFor(v => v.n_passed, f => short.Parse(f.Random.Number(0,5).ToString()));
                var diagnosesCount = new Faker().Random.Number(1,3);
                for (int i = 0; i < diagnosesCount; i++)
                    assignedProcedures.Add(diagnosesRules.Generate());
            }

            return assignedProcedures;
        }
    }
}
