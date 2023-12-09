namespace AdventOfCode
{
    public class Day09Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input) => input.Split("\n").Select(ParseToLong).Select(Extrapolate).Sum();
        public long GetResultPart2(string input) => input.Split("\n").Select(ParseToLong).Select(ExtrapolateBackwards).Sum();

        private static long Extrapolate(long[] numbers) => !numbers.Any() ? 0 : Extrapolate(Difference(numbers)) + numbers.Last();

        private static long ExtrapolateBackwards(long[] numbers) => Extrapolate(numbers.Reverse().ToArray());

        private static long[] Difference(long[] numbers) => numbers.Zip(numbers.Skip(1)).Select(x => x.Second - x.First).ToArray();

        private static long[] ParseToLong(string line) => line.Split(" ").Select(long.Parse).ToArray();
    }
}
