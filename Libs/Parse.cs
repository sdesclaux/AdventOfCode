using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Parse
    {
        public static long[] ParseStringToLongArray(string input)
        {
            var matches = Regex.Matches(input, @"\d+");
            return matches.Select(match => long.Parse(match.Value)).ToArray();
        }
    }
}
