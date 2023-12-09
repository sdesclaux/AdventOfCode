namespace AdventOfCode
{
    public class Day07Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input)
        {
            var rankedBids = GetRankedBids(input, GetHandValue);

            long result = rankedBids.Select((bid, rank) => (rank + 1) * bid).Sum();

            return result;
        }

        public long GetResultPart2(string input)
        {
            var rankedBids = GetRankedBids(input, GetHandValueWithJoker);

            long result = rankedBids.Select((bid, rank) => (rank + 1) * bid).Sum();

            return result;
        }

        private static IEnumerable<int> GetRankedBids(string input, Func<string, string> function)
        {
            var rankedBids = (
                from line in input.Split("\n")
                let hand = line.Split(" ")[0]
                let bid = line.Split(" ")[1]
                orderby function(hand)
                select int.Parse(bid)
                );

            return rankedBids;
        }

        private static string GetHandValue(string hand)
        {
            return string.Join("", PatternValue(hand)) + string.Join("", CardValue(hand, "23456789TJQKA"));
        }

        private static string GetHandValueWithJoker(string hand)
        {
            string handWithJokerTransformed = GetHandWithJokerTransformed(hand);
            return string.Join("", PatternValue(handWithJokerTransformed)) + string.Join("", CardValue(hand, "J23456789TQKA"));
        }

        private static string[] CardValue(string hand, string cardOrder) =>
             hand.Select(ch => ((byte)(cardOrder.IndexOf(ch) + 10)).ToString()).ToArray();

        // replace cards with the number of their occurrences in the hand then order them such as
        // A8A8A becomes 33322, 9A34Q becomes 11111 and K99AA becomes 22221
        private static string[] PatternValue(string hand) => hand.Select(ch => (byte)hand.Count(x => x == ch)).OrderByDescending(_ => _).Select(_ => _.ToString()).ToArray();

        private static string GetHandWithJokerTransformed(string hand)
        {
            char replaceChar = 'J';

            var groupedChars = hand.Where(ch => ch != 'J').GroupBy(ch => ch).OrderByDescending(group => group.Count());
            if (groupedChars.Any())
            {
                replaceChar = groupedChars.First().Key;
            }

            return hand.Replace('J', replaceChar);
        }
    }
}
