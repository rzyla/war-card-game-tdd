using System.Text;
using war_card_game_tdd.Enums;
using war_card_game_tdd.Models;
using war_card_game_tdd.Utils;

namespace war_card_game_tdd
{
    public class Game
    {
        public int Iteration { get; set; } = 0;
        public List<Player> Players { get; set; }
        public List<Pool> Pool { get; set; }
        public IEnumerable<Value> Values { get; set; }

        public Game(int players)
        {
            Players = PlayerUtils
                .Create(players)
                .Names();

            DeckUtils
                .New()
                .Shuffle()
                .Split(Players);

            Pool = PoolUtils
                .Create(Players);

            Values = Enum.GetValues(typeof(Value))
                   .Cast<Value>()
                   .Reverse();
        }

        public void GetCardsFromPlayers(bool onlyAddCardToPool = false)
        {
            IfPlayerHasntCardAddDiscardToCardsAndShuffle();
            IfPlayerHasntCardSetAddAllRequiredCardsFalse();

            var players = Players.Where(w => w.Cards.Count > 0).ToList();

            if (!players.Any())
            {
                throw new ApplicationException("No available players");
            }

            foreach (var player in players)
            {
                Pool.AddCard(player.Name, player.GetCard());

                if(onlyAddCardToPool)
                {
                    IfPlayerHasntCardSetAddAllRequiredCardsFalse();
                    Pool.AddCard(player.Name, player.GetCard());
                }
            }

            if (!onlyAddCardToPool)
            {
                if (!ComparePool())
                {
                    GetCardsFromPlayers(true);
                }

                Iteration++;
            }
        }

        public void IfPlayerHasntCardAddDiscardToCardsAndShuffle()
        {
            var players = Players.Where(w => w.Cards.Count == 0 && w.Discard.Count > 0).ToList();

            if(players != null)
            {
                foreach(var player in players)
                {
                    player.AddDiscardToCards();
                    var cards = player.Cards;
                    player.Cards = cards.Shuffle();
                }
            }
        }

        public void IfPlayerHasntCardSetAddAllRequiredCardsFalse()
        {
            var players = Players.Where(w => w.Cards.Count == 0).ToList();

            if (players != null)
            {
                foreach (var player in players)
                {
                    Pool.GetPoolByPlayerName(player.Name)
                        .AddAllRequiredCards = false;
                }
            }
        }

        public bool ComparePool()
        {
            foreach (var value in Values)
            {
                var list = Pool.Where(w => w.Cards.Count > 0 
                    && w.Cards.Last().Value.Equals(value) 
                    && w.AddAllRequiredCards == true);

                if (list.Any())
                {
                    var count = list.Count();

                    if (count > 0)
                    {
                        return count == 1;
                    }
                }
            }

            return false;
        }

        public void AddCardsToWinner()
        {
            foreach (var value in Values)
            {
                var list = Pool.Where(w => w.Cards.Count > 0
                    && w.Cards.Last().Value.Equals(value)
                    && w.AddAllRequiredCards == true);

                if (list.Count() == 1)
                {
                    var winner = list.First();
                    var player = Players.GetPlayerByName(winner.Name);

                    foreach (var pool in Pool)
                    {
                        player.Discard.AddRange(pool.Cards);
                    }

                    Pool.Clean();
                }
            }
        }

        public Player? Winner()
        {
            if (Players.Any())
            {
                var playersWithAvailableCards = Players
                    .Where(w => !(w.Cards.Count + w.Discard.Count).Equals(0))
                    .Count();

                if (playersWithAvailableCards == 1)
                {
                    return Players
                        .Where(w => !(w.Cards.Count + w.Discard.Count).Equals(0))
                        .First();
                }
            }

            return null;
        }

        public string Display()
        {
            var sb = new StringBuilder();

            if (Players.Any())
            {
                foreach (var pool in Pool)
                {
                    var cards = pool.Cards.Select(s => s.Value);
                    sb.Append(string.Format("{0}: {1}{2}", pool.Name, string.Join(" ", cards), Environment.NewLine));
                }

                sb.Append(string.Format("{0}", Environment.NewLine));

                foreach (var player in Players)
                {
                    sb.Append(string.Format("{0} ({1}\\{2}){3}", player.Name, player.Cards.Count, player.Discard.Count, Environment.NewLine));

                    foreach (var value in Values)
                    {
                        var sum = player.Cards.Where(w => w.Value.Equals(value)).Count()
                            + player.Discard.Where(w => w.Value.Equals(value)).Count();

                        sb.Append(String.Format("{0}:{1} ", value, sum));
                    }

                    sb.Append(string.Format("{0}{0}", Environment.NewLine));
                }
            }

            return sb.ToString();
        }
    }
}
