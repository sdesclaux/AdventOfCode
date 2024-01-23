namespace AdventOfCode
{
    public class Day14Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input)
        {
            char[][] map = Parse(input);
            map = RollRocksNorth(map);
            long result = CalculateResultFromMap(map);
            return result;
        }

        public long GetResultPart2(string input)
        {
            char[][] map = Parse(input);
            map = IterateCycles(map , 1000000000);
            long result = CalculateResultFromMap(map);
            return result;
        }

        private static char[][] Parse(string input) =>
        (from line in input.Split('\n') select line.ToCharArray()).ToArray();

        private static char[][] RollRocksNorth(char[][] map)
        {
            var moved = true;
            while (moved)
            {
                moved = false;
                for (var irow = 0; irow < map.Length - 1; irow++)
                {
                    for (var icol = 0; icol < map[0].Length; icol++)
                    {
                        if (map[irow][icol] == '.' && map[irow + 1][icol] == 'O')
                        {
                            map[irow][icol] = 'O';
                            map[irow + 1][icol] = '.';
                            moved = true;
                        }
                    }
                }
            }
            return map;
        }

        private static int CalculateResultFromMap(char[][] map)
        {
            return (
                from irow in Enumerable.Range(0, map.Length)
                from icol in Enumerable.Range(0, map[0].Length)
                where map[irow][icol] == 'O'
                select map.Length - irow
            ).Sum();
        }

        private static char[][] IterateCycles(char[][] map, long iterationCount)
        {
            // Done here to find if i am in a loop (i will)
            var hash = (char[][] map) => string.Join("", from line in map from ch in line select ch);

            var seen = new List<string>();
            while (!seen.Contains(hash(map)) && iterationCount > 0)
            {
                seen.Add(hash(map));
                map = Cycle(map);
                iterationCount--;
            }

            //Theloop is found so do the lasts iteration du find the last position
            var loopLength = seen.Count - seen.IndexOf(hash(map));
            iterationCount %= loopLength;

            while (iterationCount > 0)
            {
                map = Cycle(map);
                iterationCount--;
            }
            return map;
        }

        private static char[][] Cycle(char[][] map)
        {
            //We cycle nut we don't create a goeast/west/south, we rotate the map
            for (var i = 0; i < 4; i++)
            {
                map = Rotate(RollRocksNorth(map));
            }
            return map;
        }

        private static char[][] Rotate(char[][] map)
        {
            var dst = new char[map.Length][];
            for (var irow = 0; irow < map[0].Length; irow++)
            {
                dst[irow] = new char[map[0].Length];
                for (var icol = 0; icol < map.Length; icol++)
                {
                    dst[irow][icol] = map[map.Length - icol - 1][irow];
                }
            }
            return dst;
        }
    }
}
