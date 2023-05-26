using war_card_game_tdd.Models;

namespace war_card_game_tdd.Utils
{
    public static class PoolUtils
    {
        public static List<Pool> Create(List<Player> players)
        {
            var pool = new List<Pool>();

            foreach (var player in players)
            {
                pool.Add(new Pool(player.Name));
            }

            return pool;
        }

        public static void Clean(this List<Pool> pool)
        {
            foreach(var item in pool)
            {
                item.Cards = new List<Card>();
                item.AddAllRequiredCards = null;
            }
        }

        public static Pool GetPoolByPlayerName(this List<Pool> pool, string name)
        {
            return pool.Where(w => w.Name.Equals(name)).First();
        }

        public static void AddCard(this List<Pool> list, string name, Card? card)
        {
            var pool = GetPoolByPlayerName(list, name);

            if(card == null)
            {
                pool.AddAllRequiredCards = false;
            }
            else
            {
                pool.AddAllRequiredCards = true;
                pool.Cards.Add(card);
            }
        }
    }
}
