namespace AdventOfCode
{
    public class Day06Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input)
        {
            var rows = input.Split("\n");
            var times = Parse.ParseStringToLongArray(rows[0]);
            var distances = Parse.ParseStringToLongArray(rows[1]);

            long result = 1;
            for (var i = 0; i < times.Length; i++)
            {
                result *= GetWinningsCount(times[i], distances[i]);
            }
            return result;
        }

        public long GetResultPart2(string input)
        {
            input = input.Replace(" ", "");
            var rows = input.Split("\n");
            var times = Parse.ParseStringToLongArray(rows[0]);
            var distances = Parse.ParseStringToLongArray(rows[1]);

            long result = 1;
            for (var i = 0; i < times.Length; i++)
            {
                result *= GetWinningsCount(times[i], distances[i]);
            }
            return result;
        }

        private static long GetWinningsCount(long time, long distance)
        {
            // We win if we go further than distance
            // We go to x * (time - x)
            // so we need to have x * (time - x) > distance => -x² + x*time - distance > 0
            // this si a second order equation calculation
            var (x1, x2) = SecondOrderEquationResults(-1, time, -distance);


            //ceil for the min
            var min = (long)Math.Ceiling(Math.Min(x1, x2));
            // floor for the max
            var max = (long)Math.Floor(Math.Max(x1, x2));

            //return count between max and min
            return max - min + 1;
        }

        private static (double, double) SecondOrderEquationResults(long a, long b, long c)
        {
            var delta = b * b - 4 * a * c;
            var deltaSquared = Math.Sqrt(delta);
            var x1 = (-b - deltaSquared) / (2 * a);
            var x2 = (-b + deltaSquared) / (2 * a);
            return (x1, x2);
        }
    }
}
