using war_card_game_tdd.Enums;

namespace war_card_game_tdd.Models
{
    public class Card
    {
        public Color Color { get; set; }
        public Value Value { get; set; }

        public Card(Color color, Value value)
        {
            Color = color;
            Value = value;
        }
    }
}
