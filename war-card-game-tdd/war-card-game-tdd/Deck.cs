using war_card_game_tdd.Enums;
using war_card_game_tdd.Models;

namespace war_card_game_tdd
{
    public class Deck
    {
        private List<Card> Cards { get; set; } = new List<Card>();

        public List<Card> New()
        {
            Cards = new List<Card>();

            var colors = Enum.GetValues(typeof(Color));
            var values = Enum.GetValues(typeof(Value));

            foreach(Value value in values)
            {
                foreach(Color color in colors)
                {
                    Cards.Add(new Card(color, value));
                }
            }

            return Cards;
        }

        public List<Card> Get()
        {
            return Cards;
        }

        public List<Card> Shuffle()
        {
            var count = Cards.Count;
            var random = new Random();
            var shuffledCards = new List<Card>();
            
            for(var i = 0; i < count; i++)
            {
                var _random = random.Next(0, Cards.Count);
                shuffledCards.Add(Cards[_random]);
                Cards.Remove(Cards[_random]);
            }

            Cards = shuffledCards;
            return Cards;
        }
    }
}
