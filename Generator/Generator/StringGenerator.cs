using System;
using System.Linq;

namespace Generator.Generator

{

    public class StringGenerators
    {
        private const string _chars = "abcdefghijklmnopqrstuvwxyz1234567890";
        
        static readonly Random _rand = new Random(DateTime.Now.Millisecond);
        
        public static string Generate(int length, bool space = false, string charset = _chars)
        {
            return new string(Enumerable.Repeat(space ? charset + " " : charset, length)
                .Select(s => s[_rand.Next(s.Length)]).ToArray());
        }
    }
}
