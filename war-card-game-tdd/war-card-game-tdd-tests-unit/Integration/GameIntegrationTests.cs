using war_card_game_tdd;
using war_card_game_tdd.Enums;
using war_card_game_tdd.Models;

namespace war_card_game_tdd_tests.Integration
{
    public class GameIntegrationTests
    {
        [Test]
        public void Game_ShouldCreateExpectedNumbersOfPlayers([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange & Act
            var game = new Game(players);

            // Assert
            Assert.That(game.Players, Has.Count.EqualTo(players));
        }

        [Test]
        public void Game_ShouldCreateDeckAndSplitToPlayers([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var colors = Enum.GetValues(typeof(Color));
            var values = Enum.GetValues(typeof(Value));
            var expectedElements = colors.Length * values.Length;

            // Act
            var game = new Game(players);
            var cards = game.Players.Select(s => s.Cards.Count).Sum();

            // Assert
            Assert.That(cards, Is.EqualTo(expectedElements));
        }

        [Test]
        public void Game_IfPlayerHasntCardAddDiscardToCards_SumPlayersCardsEqualsMultiplicationOfColorAndValue([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var colors = Enum.GetValues(typeof(Color));
            var values = Enum.GetValues(typeof(Value));
            var expectedElements = colors.Length * values.Length;
            var game = new Game(players);
            var player = game.Players.First();

            // Act
            player.Discard = player.Cards;
            player.Cards = new List<Card>();

            game.IfPlayerHasntCardAddDiscardToCardsAndShuffle();

            var sum = game.Players.Select(s => s.Cards.Count).Sum();

            // Assert
            Assert.That(sum, Is.EqualTo(expectedElements));
        }
    }
}
