namespace AdventOfCode
{
    public class Day05Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input)
        {
            var groups = input.Split("\n\n");
            long[] seeds = groups[0].Split(":")[1].Trim().Split(" ").Select(_ => long.Parse(_)).ToArray();

            List<long> locations = new List<long>();
            List<Ranges> rangesList = new List<Ranges>();
            for (int i = 1; i < groups.Length; i++)
            {
                var group = groups[i];
                var rangesString = group.Split("\n");
                List<Range> rangeList = new List<Range>();
                for (int j = 1; j < rangesString.Length; j++)
                {
                    var range = ParseRange(rangesString[j]);
                    rangeList.Add(range);
                }
                var ranges = new Ranges(rangeList.ToArray());
                rangesList.Add(ranges);
            }

            for (int i = 0; i < seeds.Length; i++)
            {
                long seed = seeds[i];
                for (int j = 0; j < rangesList.Count; j++)
                {
                    var ranges = rangesList[j];
                    seed = (from r in ranges.ranges
                            where r.sourceStart <= seed && seed < (r.sourceStart + r.length)
                            select seed + r.destinationStart - r.sourceStart).FirstOrDefault(seed);
                }
                locations.Add(seed);
            }

            return locations.ToArray().Min();
        }

        // Optimisation needed /!\ surely not the way to solve it (>40 min treatment)
        public long GetResultPart2(string input)
        {
            var groups = input.Split("\n\n");
            long[] inputSeeds = groups[0].Split(":")[1].Trim().Split(" ").Select(_ => long.Parse(_)).ToArray();
            List<long> seedsList = new List<long>();

            for (int i = 0; i < inputSeeds.Length; i += 2)
            {
                var seedstart = inputSeeds[i];
                var seedlegth = inputSeeds[i + 1];
                List<long> list = new List<long>();
                for (long j = seedstart; j < seedstart + seedlegth; j++)
                {
                    list.Add(j);
                }
                seedsList.AddRange(list.ToArray());
            }

            long[] seeds = seedsList.ToArray();

            List<long> locations = new List<long>();
            List<Ranges> rangesList = new List<Ranges>();
            for (int i = 1; i < groups.Length; i++)
            {
                var group = groups[i];
                var rangesString = group.Split("\n");
                List<Range> rangeList = new List<Range>();
                for (int j = 1; j < rangesString.Length; j++)
                {
                    var range = ParseRange(rangesString[j]);
                    rangeList.Add(range);
                }
                var ranges = new Ranges(rangeList.ToArray());
                rangesList.Add(ranges);
            }

            for (int i = 0; i < seeds.Length; i++)
            {
                long seed = seeds[i];
                for (int j = 0; j < rangesList.Count; j++)
                {
                    var ranges = rangesList[j];
                    seed = (from r in ranges.ranges
                            where r.sourceStart <= seed && seed < (r.sourceStart + r.length)
                            select seed + r.destinationStart - r.sourceStart).FirstOrDefault(seed);
                }
                locations.Add(seed);
            }

            return locations.ToArray().Min();
        }

        record Range(long destinationStart, long sourceStart, long length);
        record Ranges(Range[] ranges);
        static Range ParseRange(string line)
        {
            var ranges = line.Split(' ').Select(_ => long.Parse(_)).ToArray();
            return new Range(ranges[0], ranges[1], ranges[2]);
        }
    }
}
