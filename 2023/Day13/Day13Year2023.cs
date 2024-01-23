using System.Collections.Immutable;
using System.Numerics;
using static System.Formats.Asn1.AsnWriter;
using Map = System.Collections.Immutable.ImmutableDictionary<System.Numerics.Complex, char>;

namespace AdventOfCode
{
    public class Day13Year2023 : IResultGenerator
    {
        Complex Right = Complex.One;
        Complex Down = Complex.ImaginaryOne;

        public long GetResultPart1(string input)
        {
            return input.Split("\n\n").Select(ParseMap).Select(GetScores).Sum(); ;
        }

        public long GetResultPart2(string input)
        {
            var rows = input.Split("\n");
            long result = 0;

            return result;
        }

        private static int GetScores(Map map)
        {
            //return new[] {
            //.. GetMirrors(map, Down),
            //.. GetMirrors(map, Right).Select(r => r * 100),
            //}.Single();
            return 0;
        }

        private static Map ParseMap(string input)
        {
            var rows = input.Split("\n");
            return (
                from irow in Enumerable.Range(0, rows.Length)
                from icol in Enumerable.Range(0, rows[0].Length)
                let pos = new Complex(icol, irow)
                let cell = rows[irow][icol]
                select new KeyValuePair<Complex, char>(pos, cell)
            ).ToImmutableDictionary();
        }
    }
}
