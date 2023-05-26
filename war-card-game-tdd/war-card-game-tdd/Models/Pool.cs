namespace war_card_game_tdd.Models
{
    public class Pool
    {
        public string Name { get; set; }
        public bool? AddAllRequiredCards { get; set; }
        public List<Card> Cards { get; set; }

        public Pool(string name)
        {
            Name = name;
            Cards = new List<Card>();
        }
    }
}
