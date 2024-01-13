using Bogus;
using Generator.Models;
using System;
using System.Collections.Generic;

namespace Generator.Generator
{
    internal class VacationerGenerator
    {
        static List<Vacationers> vacationers = new List<Vacationers>();
        Random r = new Random();
        public static List<Vacationers> Generate(int count, List<Workers> workers)
        {
            var dateBirth = new DateGenerator(new DateTime(1970, 1, 1));
            var dateArrive = new DateGenerator(new DateTime(2020, 1, 1));
            var vacationerRules = new Faker<Vacationers>()
               .StrictMode(true)
               .RuleFor(v => v.snn, f => StringGenerators.Generate(10))
               .RuleFor(v => v.skk_code, f => f.UniqueIndex)
               .RuleFor(v => v.fullname, f => f.Name.FullName())
               .RuleFor(v => v.birth, f => dateBirth.Next())
               .RuleFor(v => v.city, f => f.Address.City())
               .RuleFor(v => v.responsible_doctor, f => f.PickRandom(workers).id)
               .RuleFor(v => v.arriving_date, f => dateArrive.Next())
               .RuleFor(v => v.leaving_date, f => dateArrive.AddDays(f.Random.Number(10, 20)))
               .RuleFor(v => v.revision_date, f => dateArrive.AddDays(f.Random.Number(5, 10)))
               .RuleFor(v => v.corpus, f => StringGenerators.Generate(10))
               .RuleFor(v => v.room, f => f.Random.Number(0, 450).ToString());

            for (int i = 0; i < count; ++i)
                vacationers.Add(vacationerRules.Generate());
            return vacationers;
        }
    }
}
