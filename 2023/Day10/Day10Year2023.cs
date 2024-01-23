using System.Numerics;

namespace AdventOfCode
{
    public class Day10Year2023 : IResultGenerator
    {
        static readonly Complex Left = -Complex.One;
        static readonly Complex Right = Complex.One;
        static readonly Complex Up = -Complex.ImaginaryOne;
        static readonly Complex Down = Complex.ImaginaryOne;
        static readonly Complex[] Dirs = new[]{Up, Right, Down, Left };

        public long GetResultPart1(string input)
        {
            var map = ParseInput(input);
            var loop = LoopPositions(map);
            return loop.Count / 2;
        }

        public long GetResultPart2(string input)
        {
            var map = ParseInput(input);
            var loop = LoopPositions(map);
            return map.Keys.Count(k => IsInsideLoop(k, map, loop));
        }

        private static Dictionary<Complex, char> ParseInput(string input)
        {
            var rows = input.Split("\n");
            var crow = rows.Length;
            var ccol = rows[0].Length;
            var res = new Dictionary<Complex, char>();
            for (var irow = 0; irow < crow; irow++)
            {
                for (var icol = 0; icol < ccol; icol++)
                {
                    res[new Complex(icol, irow)] = rows[irow][icol];
                }
            }
            return res;
        }

        private static HashSet<Complex> LoopPositions(Dictionary<Complex, char> map)
        {
            var start = map.Keys.Single(k => map[k] == 'S');
            var positions = new HashSet<Complex>() { };
            //On cherche une première direction dans laquelle partir depuis S en itérant sur les 4 direcitons possibles
            Complex dir = Dirs.First(d => DirsFrom(map[start + d]).Contains(d));

            positions.Add(start);
            var nextPosition = start + dir;

            while (nextPosition != start)
            {
                positions.Add(nextPosition);
                dir = DirsExit(map[nextPosition]).Single(dirOut => dirOut != -dir); // On récupère celle dont l'on ne vient pas
                nextPosition += dir; // On se déplace grâce aux complexes
            }

            return positions;
        }

        // If i am on this cell, i have these exits
        private static Complex[] DirsExit(char ch) => ch switch
        {
            '7' => new[] { Left, Down },
            'F' => new[] { Right, Down },
            'L' => new[] { Up, Right },
            'J' => new[] { Up, Left },
            '|' => new[] { Up, Down },
            '-' => new[] { Left, Right },
            'S' => new[] { Up, Down, Left, Right },
            _ => Array.Empty<Complex>()
        };

        // If i am on this cell, i have these enters
        private static Complex[] DirsFrom(char ch) =>
            DirsExit(ch).Select(ch => -ch).ToArray();

        //Here we will arbitrary go to the left and see how many time we are crossing the principal loop on a direction who blocks us : L, J, | and S
        private static bool IsInsideLoop(Complex position, Dictionary<Complex, char> map, HashSet<Complex> loop)
        {
            bool isInside = false;
            int crossingLoopCount = 0;

            if (!loop.Contains(position)) // We don't treat if we are on the loop at the start
            {
                position += Left; //Go to the left

                while (map.ContainsKey(position))
                {
                    // It doesn't always work well with the S, it depends of it's "real" value
                    if (loop.Contains(position) && (map[position] == 'L' || map[position] == 'J' || map[position] == '|' || map[position] == 'S')) {
                        crossingLoopCount++;
                    }
                    position += Left;
                }

                isInside = crossingLoopCount % 2 != 0; //We check how many time we cross the loop : odd we are outside, even we are inside
            }

            return isInside;
        }
    }
}
