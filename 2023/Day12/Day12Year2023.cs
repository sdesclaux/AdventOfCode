using System.Collections.Immutable;

using Cache = System.Collections.Generic.Dictionary<(string, System.Collections.Immutable.ImmutableStack<int>), long>;
namespace AdventOfCode
{
    public class Day12Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input)
        {
            return GetResult(input, 1);
        }

        public long GetResultPart2(string input)
        {
            return GetResult(input, 5);
        }

        private static long GetResult(string input, int repeat)
        {
            long result = 0;
            foreach (string line in input.Split("\n"))
            {
                var parts = line.Split(" ");
                var pattern = RepeatInputWithSeparator(parts[0], '?', repeat);
                var nums = RepeatInputWithSeparator(parts[1], ',', repeat).Split(',').Select(int.Parse);
                result += Compute(pattern, ImmutableStack.CreateRange(nums.Reverse()), new Cache());
            }
            return result;
        }

        private static string RepeatInputWithSeparator(string input, char separator, int repeat)
        {
            return string.Join(separator, Enumerable.Repeat(input, repeat));
        }

        private static long Compute(string pattern, ImmutableStack<int> nums, Cache cache)
        {
            if (!cache.ContainsKey((pattern, nums)))
            {
                cache[(pattern, nums)] = Dispatch(pattern, nums, cache);
            }
            return cache[(pattern, nums)];
        }

        private static long Dispatch(string pattern, ImmutableStack<int> nums, Cache cache)
        {
            return pattern.FirstOrDefault() switch
            {
                '.' => ProcessDot(pattern, nums, cache),
                '?' => ProcessQuestion(pattern, nums, cache),
                '#' => ProcessHash(pattern, nums, cache),
                _ => ProcessEnd(pattern, nums, cache),
            };
        }

        private static long ProcessEnd(string _, ImmutableStack<int> nums, Cache __)
        {
            // the good case is when there are no numbers left at the end of the pattern
            return nums.Any() ? 0 : 1;
        }

        private static long ProcessDot(string pattern, ImmutableStack<int> nums, Cache cache)
        {
            // consume one spring and recurse
            return Compute(pattern[1..], nums, cache);
        }

        private static long ProcessQuestion(string pattern, ImmutableStack<int> nums, Cache cache)
        {
            // recurse both ways
            return Compute("." + pattern[1..], nums, cache) + Compute("#" + pattern[1..], nums, cache);
        }

        private static long ProcessHash(string pattern, ImmutableStack<int> nums, Cache cache)
        {
            // take the first number and consume that many dead springs, recurse

            if (!nums.Any())
            {
                return 0; // no more numbers left, this is no good
            }

            var n = nums.Peek();
            nums = nums.Pop();

            var potentiallyDead = pattern.TakeWhile(s => s == '#' || s == '?').Count();

            if (potentiallyDead < n)
            {
                return 0; // not enough dead springs 
            }
            else if (pattern.Length == n)
            {
                return Compute("", nums, cache);
            }
            else if (pattern[n] == '#')
            {
                return 0; // dead spring follows the range -> not good
            }
            else
            {
                return Compute(pattern[(n + 1)..], nums, cache);
            }
        }
    }
}
