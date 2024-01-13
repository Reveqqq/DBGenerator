using Bogus;
using Generator.Models;
using System;
using System.Collections.Generic;

namespace Generator.Generator
{
    internal class DiagnosesGenerator
    {
        static List<Diagnoses> diagnoses = new List<Diagnoses>();
        Random r = new Random();
        public static List<Diagnoses> Generate(List<Vacationers> vacationers, List<Diseases> diseases)
        {
            foreach (var vacationer in vacationers)
            {
                var diagnosesRules = new Faker<Diagnoses>()
               .StrictMode(true)
               .RuleFor(v => v.snn, f => vacationer.snn)
               .RuleFor(v => v.skk_code, f =>  vacationer.skk_code)
               .RuleFor(v => v.disease_id, f => f.PickRandom(diseases).id);
                diagnoses.Add(diagnosesRules.Generate());
            }
                         
            return diagnoses;
        }
    }
}
