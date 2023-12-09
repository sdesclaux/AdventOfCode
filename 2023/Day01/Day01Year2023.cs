using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day01Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input)
        {
            var rows = input.Split("\n");
            int sum = 0;
            foreach (var row in rows)
            {
                int.TryParse(Regex.Match(row, @"\d").Value, out int firstdigit);
                int.TryParse(Regex.Match(row, @"\d", RegexOptions.RightToLeft).Value, out int lastDigit);
                int result = int.Parse((firstdigit.ToString() + lastDigit.ToString()));
                sum += result;
            }

            return sum;
        }

        public long GetResultPart2(string input)
        {
            var rows = input.Split("\n");
            int sum = 0;
            foreach (var item in rows)
            {
                int firstdigit = FindFirstDigit(item);
                int lastDigit = FindLastDigit(item);
                int result = int.Parse((firstdigit.ToString() + lastDigit.ToString()));
                sum += result;
            }

            return sum;
        }

        private readonly string[] elements = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        private int FindFirstDigit(string input)
        {
            Dictionary<string, int> dictionary = elements.Select((element, index) => new { Key = element, Value = index + 1 }).ToDictionary(x => x.Key, x => x.Value);
           
            string pattern = @"(\d|" + string.Join("|", elements) + ")";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                string value = match.Value.ToString();
                if (value.Length > 1)
                {
                    return dictionary[value];
                }
                else
                {
                    return int.Parse(value);
                }
            }

            return 0;
        }

        private int FindLastDigit(string input)
        {
            Dictionary<string, int> dictionary = elements.Select((element, index) => new { Key = element, Value = index + 1 }).ToDictionary(x => x.Key, x => x.Value);

            string pattern = @".*(\d+|" + string.Join("|", elements) + @")";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                string value = match.Groups.Values.Last().ToString();
                if (value.Length > 1)
                {
                    return dictionary[value];
                }
                else
                {
                    return int.Parse(value);
                }
            }

            return 0;
        }
    }
}
