using war_card_game_tdd.Enums;
using war_card_game_tdd.Models;

namespace war_card_game_tdd.Utils
{
    public static class DeckUtils
    {
        public static List<Card> New()
        {
            var cards = new List<Card>();
            var colors = Enum.GetValues(typeof(Color));
            var values = Enum.GetValues(typeof(Value));

            foreach (Value value in values)
            {
                foreach (Color color in colors)
                {
                    cards.Add(new Card(color, value));
                }
            }

            return cards;
        }

        public static List<Card> Shuffle(this List<Card> cards)
        {
            var count = cards.Count;
            var random = new Random();
            var shuffledCards = new List<Card>();

            for (var i = 0; i < count; i++)
            {
                var _random = random.Next(0, cards.Count);
                shuffledCards.Add(cards[_random]);
                cards.Remove(cards[_random]);
            }

            return shuffledCards;
        }

        public static void Split(this List<Card> cards, IEnumerable<Player> players)
        {
            if (players.Any())
            {
                foreach (var player in players)
                {
                    player.Cards = new List<Card>();
                }

                while (cards.Count > 0)
                {
                    foreach (var player in players)
                    {
                        if (cards.Count > 0)
                        {
                            var card = cards.First();
                            player.AddToCards(card);
                            cards.Remove(card);
                        }
                    }
                }
            }
        }
    }
}
