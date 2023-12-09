using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day04Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input)
        {
            var rows = input.Split("\n");
            List<long> points = new();

            foreach (var r in rows)
            {
                var winningCardCount = GetWinningCardCountFromInputLine(r);
                if (winningCardCount > 0)
                {
                    points.Add((long)Math.Pow(2, winningCardCount - 1));
                }
            }

            return points.Sum();
        }

        public long GetResultPart2(string input)
        {
            var rows = input.Split("\n");
            int[] winningCardCount = rows.Select(r => GetWinningCardCountFromInputLine(r)).ToArray();
            int[] scores = winningCardCount.Select(card => 1).ToArray();

            for (int i = 0; i < winningCardCount.Length; i++)
            {
                var (card, count) = (winningCardCount[i], scores[i]);
                for (int j = 0; j < card; j++)
                {
                    scores[i + j + 1] += count;
                }
            }

            return scores.Sum();
        }

        static int GetWinningCardCountFromInputLine(string line)
        {
            var parts = line.Split(':', '|');
            var l = from m in Regex.Matches(parts[1], @"\d+") select m.Value;
            var r = from m in Regex.Matches(parts[2], @"\d+") select m.Value;
            return l.Intersect(r).Count();
        }
    }
}
