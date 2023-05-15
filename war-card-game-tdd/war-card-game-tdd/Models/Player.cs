namespace war_card_game_tdd.Models
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<Card> Discard { get; set; } = new List<Card>();

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }
    }
}
