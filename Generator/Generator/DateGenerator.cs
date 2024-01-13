using System;

namespace Generator.Generator
{
    public class DateGenerator
    {
        DateTime start;
        Random gen;
        int range;

        public DateGenerator(DateTime startDate)
        {
            start = startDate;
            gen = new Random();
            range = (DateTime.Today - start).Days;
        }

        public DateTime Next()
        {
            return start
                .AddDays(gen.NextGaussian())
                .AddHours(gen.NextGaussian())
                .AddMinutes(gen.NextGaussian())
                .AddSeconds(gen.NextGaussian());
        }

        public DateTime AddDays(int value)
        {
            return start.AddDays(value);
        }
    }
}
