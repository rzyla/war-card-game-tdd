using war_card_game_tdd.Models;
using war_card_game_tdd.Utils;

namespace war_card_game_tdd
{
    public class Game
    {
        public int Iteration { get; private set; } = 0;
        public List<Player> Players { get; private set; } = new List<Player>();

        public Game(int players)
        {
            Players = PlayerUtils
                .Create(players)
                .Names();

            DeckUtils
                .New()
                .Shuffle()
                .Split(Players);
        }

        public bool Play()
        {
            throw new NotImplementedException();
        }

        public Player Winner()
        {
            throw new Exception();
        }
    }
}
