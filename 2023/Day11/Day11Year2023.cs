namespace AdventOfCode
{
    public class Day11Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input)
        {
            return GetResult(input, 2);
        }

        public long GetResultPart2(string input)
        {
            return GetResult(input, 1000000);
        }

        private static long GetResult(string input, int galaxyExpansionVelocity)
        {
            long result = 0;
            var map = input.Split("\n");

            Func<int, bool> emptyRow = GetEmptyRows(map).ToHashSet().Contains; //Function here with hashset to boost performances
            Func<int, bool> emptyCol = GetEmptyColumns(map).ToHashSet().Contains;
            var stars = FindAllGalaxies(map);

            foreach (var star1 in stars)
            {
                foreach (var star2 in stars)
                {
                    result += DistanceWithGalaxyExpansion(star1.RowIndex, star2.RowIndex, emptyRow, galaxyExpansionVelocity) +
                              DistanceWithGalaxyExpansion(star1.ColumnIndex, star2.ColumnIndex, emptyCol, galaxyExpansionVelocity);
                }
            };

            return result / 2;
        }

        private static long DistanceWithGalaxyExpansion(int i1, int i2, Func<int, bool> isEmpty, int galaxyExpansionVelocity)
        {
            var start = Math.Min(i1, i2);
            var dist = Math.Abs(i1 - i2);
            return dist + (galaxyExpansionVelocity - 1) * Enumerable.Range(start, dist).Count(isEmpty); 
        }

        private static IEnumerable<int> GetEmptyRows(string[] map)
        {
            foreach (var IndexRow in Enumerable.Range(0, map.Length))
            {
                if (map[IndexRow].All(ch => ch == '.'))
                {
                    yield return IndexRow;
                }
            }

            yield break;
        }

        private static IEnumerable<int> GetEmptyColumns(string[] map)
        {
            foreach (var ColumnIndex in Enumerable.Range(0, map[0].Length))
            {
                if (map.All(row => row[ColumnIndex] == '.'))
                {
                    yield return ColumnIndex;
                }
            }

            yield break;
        }

        private static IEnumerable<Position> FindAllGalaxies(string[] map)
        {
            foreach (var IndexRow in Enumerable.Range(0, map.Length))
            {
                foreach (var ColumnIndex in Enumerable.Range(0, map[0].Length))
                {
                    if (map[IndexRow][ColumnIndex] == '#')
                    {
                        yield return new Position(IndexRow, ColumnIndex);
                    }
                }
            }

            yield break;
        }

        record Position(int RowIndex, int ColumnIndex);
    }
}
