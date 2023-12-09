namespace AdventOfCode
{
    public class Day02Year2023 : IResultGenerator
    {
        public long GetResultPart1(string input)
        {
            var lines = input.Split("\n");
            int result = 0;

            List<Game> games = new();
            int i = 1;

            foreach ( var line in lines ) { 
            Game game = new()
            {
                Numero = i,
                Sets = new List<Dictionary<string, int>>(),
            };

            string sets = line.Split(':')[1].Trim();
            string[] setss = sets.Split(';');
            foreach (string s in setss)
            {
                string[] colors = s.Split(",");
                Dictionary<string, int> setdict = new Dictionary<string, int>();
                foreach (string c in colors)
                {
                    string[] cc = c.Trim().Split(" ");
                    setdict.Add(cc[1], int.Parse(cc[0]));
                }

                game.Sets.Add(setdict);
            }

            games.Add(game);
            i++;
            }

            foreach (Game item in games)
            {
                int maxred = 12;
                int maxgreen = 13;
                int maxblue = 14;
                bool isValidGame = true;
                foreach (Dictionary<string, int> set in item.Sets)
                {
                    int red = set.ContainsKey("red") ? set["red"] : 0; ;
                    int green = set.ContainsKey("green") ? set["green"] : 0;
                    int blue = set.ContainsKey("blue") ? set["blue"] : 0; ;
                    if (isValidGame && (red > maxred || green > maxgreen || blue > maxblue))
                        isValidGame = false;
                }

                if (isValidGame)
                    result += item.Numero;
            }
            return result;
        }

        public long GetResultPart2(string input)
        {
            var lines = input.Split("\n");
            int result = 0;

            List<Game> games = new List<Game>();
            int i = 1;

            foreach (var line in lines)
            {
                Game game = new Game
                {
                    Numero = i,
                    Sets = new List<Dictionary<string, int>>(),
                };

                string sets = line.Split(':')[1].Trim();
                string[] setss = sets.Split(';');
                foreach (string s in setss)
                {
                    string[] colors = s.Split(",");
                    Dictionary<string, int> setdict = new Dictionary<string, int>();
                    foreach (string c in colors)
                    {
                        string[] cc = c.Trim().Split(" ");
                        setdict.Add(cc[1], int.Parse(cc[0]));
                    }

                    game.Sets.Add(setdict);
                }

                games.Add(game);
                i++;
            }

            foreach (Game item in games)
            {
                int maxred = 0;
                int maxgreen = 0;
                int maxblue = 0;
                foreach (Dictionary<string, int> set in item.Sets)
                {
                    int red = set.ContainsKey("red") ? set["red"] : 0; ;
                    int green = set.ContainsKey("green") ? set["green"] : 0;
                    int blue = set.ContainsKey("blue") ? set["blue"] : 0; ;
                    if (red > maxred)
                        maxred = red;
                    if (green > maxgreen)
                        maxgreen = green;
                    if (blue > maxblue)
                        maxblue = blue;
                }

                result += maxred * maxblue * maxgreen;

            }
            return result;
        }

        public record Game
        {
            public int Numero { get; set; }
            public List<Dictionary<string, int>> Sets { get; set;}
        }
    }
}
