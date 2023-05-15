using war_card_game_tdd.Models;

namespace war_card_game_tdd.Utils
{
    public static class PlayerUtils
    {
        public static List<Player> Create(int count)
        {
            var players = new List<Player>();

            for (var i = 0; i < count; i++)
            {
                players.Add(new Player());
            }

            return players;
        }

        public static List<Player> Names(this List<Player> players)
        {
            var random = new Random();
            var names = new List<string>
            {
                "Alice",
                "Connor",
                "Emma",
                "Harry",
                "James",
                "Luke",
                "Mila",
                "Oscar",
                "Rose",
                "Stacy",
                "William"
            };

            foreach (var player in players)
            {
                var name = random.Next(0, names.Count);

                player.Name = names[name];
                names.Remove(names[name]);
            }

            return players;
        }

        public static bool ValidateNumberOfPlayers(int? players)
        {
            return players >= 2 && players <= 6;
        }
    }
}
