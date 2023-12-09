using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day08Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input)
        {
            (string directions, var map) = ExtractData(input);
            var start = "AAA";
            var endRegexPattern = "ZZZ";

            return GetStepsCountFromStartToEnd(start, endRegexPattern, directions, map);
        }

        public long GetResultPart2(string input)
        {
            (string directions, var map) = ExtractData(input);
            var endRegexPattern = @"Z$";

            long result = 1;
            foreach (var key in map.Keys.Where(m => m.EndsWith("A")))
            {
                result = PlusPetitCommunMultiple(result, GetStepsCountFromStartToEnd(key, endRegexPattern, directions, map));
            }
            return result;
        }

        // TODO : Extract these in a lib for future
        //PPCM in french / LCM in english
        private static long PlusPetitCommunMultiple(long a, long b) => a * b / PlusGrandCommunDiviseur(a, b);
        //PGCD in french / GCD in english
        private static long PlusGrandCommunDiviseur(long a, long b) => b == 0 ? a : PlusGrandCommunDiviseur(b, a % b);

        private static long GetStepsCountFromStartToEnd(string start, string regexPattern, string dirs, Dictionary<string, (string, string)> map)
        {
            var i = 0;
            while (!Regex.IsMatch(start, regexPattern))
            {
                var dir = dirs[i % dirs.Length];
                start = dir == 'L' ? map[start].Item1 : map[start].Item2;
                i++;
            }
            return i;
        }

        private static (string,Dictionary<string, (string, string)>) ExtractData(string input)
        {
            var rows = input.Split("\n\n");
            string directions = rows[0];
            var map = new Dictionary<string, (string, string)>();
            foreach (var row in rows[1].Split("\n"))
            {
                var m = Regex.Matches(row, "[A-Z]+").ToArray();
                map[m[0].Value] = (m[1].Value, m[2].Value);
            }
            return (directions, map);
        }
    }
}
