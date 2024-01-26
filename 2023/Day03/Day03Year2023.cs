using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode
{
    public class Day03Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input)
        {
            var rows = input.Split("\n");
            var symbols = ParseSymbols(rows, new Regex(@"[^.0-9]"));
            var nums = ParseNumbers(rows);

            IEnumerable<int> enumerable()
            {
                return from NumberPosition n in nums
                       where symbols.Any(s => IsAdjacent(n, s))
                       select n.Number;
            }

            return enumerable().Sum();
        }

        public long GetResultPart2(string input)
        {
            var rows = input.Split("\n");
            var stars = ParseSymbols(rows, new Regex(@"\*"));
            var nums = ParseNumbers(rows);

            IEnumerable<int> enumerable()
            {
                foreach (var s in stars)
                {
                    var neighbours = from n in nums where IsAdjacent(n, s) select n.Number;
                    if (neighbours.Count() == 2)
                    {
                        yield return neighbours.First() * neighbours.Last();
                    }
                }
            }

            return enumerable().Sum();
        }

        static SymbolPosition[] ParseSymbols(string[] rows, Regex regex)
        {
            List<SymbolPosition> symbols = new();

            for (int i = 0; i < rows.Length; i++)
            {
                string row = rows[i];
                MatchCollection matches = regex.Matches(row);

                foreach (Match match in matches)
                {
                    string value = match.Value;
                    int index = match.Index;

                    SymbolPosition symbolPosition = new(value, i, index);
                    symbols.Add(symbolPosition);
                }
            }

            return symbols.ToArray();
        }

        static NumberPosition[] ParseNumbers(string[] rows)
        {
            var regex = new Regex(@"\d+");
            List<NumberPosition> numberPositions = new();

            for (int i = 0; i < rows.Length; i++)
            {
                string row = rows[i];
                MatchCollection matches = regex.Matches(row);

                foreach (Match match in matches)
                {
                    string value = match.Value;
                    int index = match.Index;

                    NumberPosition numberPosition = new(int.Parse(value), i, index);
                    numberPositions.Add(numberPosition);
                }
            }

            return numberPositions.ToArray();
        }
                static bool IsAdjacent(NumberPosition numPosition, SymbolPosition symbPosition) =>
            numPosition.ColNumb <= symbPosition.ColNumb + symbPosition.Text.Length &&
            symbPosition.ColNumb <= numPosition.ColNumb + numPosition.Number.ToString().Length &&
            symbPosition.RowNumb - numPosition.RowNumb <= 1 &&
            symbPosition.RowNumb - numPosition.RowNumb >= -1;

        record SymbolPosition(string Text, int RowNumb, int ColNumb);
        record NumberPosition(int Number, int RowNumb, int ColNumb);
    }
}
