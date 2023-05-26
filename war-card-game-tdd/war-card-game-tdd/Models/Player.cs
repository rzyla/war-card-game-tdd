namespace war_card_game_tdd.Models
{
    public class Player
    {
        public string Name { get; set; } = string.Empty;
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<Card> Discard { get; set; } = new List<Card>();

        public Card? GetCard()
        {
            if(Cards.Count > 0)
            {
                var card = Cards.First();
                Cards.Remove(card);
                return card;
            }

            return null;
        }

        public void AddToCards(Card card)
        {
            Cards.Add(card);
        }

        public void AddToDiscard(List<Card> cards)
        {
            Discard.AddRange(cards);
        }

        public void AddDiscardToCards()
        {
            Cards.AddRange(Discard);
            Discard = new List<Card>();
        }
    }
}
