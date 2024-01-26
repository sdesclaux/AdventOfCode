namespace AdventOfCode
{
    public class Day05Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input)
        {
            var groups = input.Split("\n\n");
            long[] seeds = groups[0].Split(":")[1].Trim().Split(" ").Select(_ => long.Parse(_)).ToArray();

            List<long> locations = new();
            List<Ranges> rangesList = new();
            for (int i = 1; i < groups.Length; i++)
            {
                var group = groups[i];
                var rangesString = group.Split("\n");
                List<Range> rangeList = new();
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
                    seed = (from r in ranges.RangeArr
                            where r.SourceStart <= seed && seed < (r.SourceStart + r.Length)
                            select seed + r.DestinationStart - r.SourceStart).FirstOrDefault(seed);
                }
                locations.Add(seed);
            }

            return locations.Min();
        }

        // Optimisation needed /!\ surely not the way to solve it (>40 min treatment)
        public long GetResultPart2(string input)
        {
            var groups = input.Split("\n\n");
            long[] inputSeeds = groups[0].Split(":")[1].Trim().Split(" ").Select(_ => long.Parse(_)).ToArray();
            List<long> seedsList = new();

            for (int i = 0; i < inputSeeds.Length; i += 2)
            {
                var seedstart = inputSeeds[i];
                var seedlegth = inputSeeds[i + 1];
                List<long> list = new();
                for (long j = seedstart; j < seedstart + seedlegth; j++)
                {
                    list.Add(j);
                }
                seedsList.AddRange(list.ToArray());
            }

            long[] seeds = seedsList.ToArray();

            List<long> locations = new();
            List<Ranges> rangesList = new();
            for (int i = 1; i < groups.Length; i++)
            {
                var group = groups[i];
                var rangesString = group.Split("\n");
                List<Range> rangeList = new();
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
                    seed = (from r in ranges.RangeArr
                            where r.SourceStart <= seed && seed < (r.SourceStart + r.Length)
                            select seed + r.DestinationStart - r.SourceStart).FirstOrDefault(seed);
                }
                locations.Add(seed);
            }

            return locations.Min();
        }

        record Range(long DestinationStart, long SourceStart, long Length);
        record Ranges(Range[] RangeArr);
        static Range ParseRange(string line)
        {
            var ranges = line.Split(' ').Select(_ => long.Parse(_)).ToArray();
            return new Range(ranges[0], ranges[1], ranges[2]);
        }
    }
}
