using war_card_game_tdd;

namespace war_card_game_tdd_tests.Unit
{
    public class GameUnitTests
    {
        [Test]
        public void Winner_ShouldReturnEmptyElement([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var winner = game.Winner();

            // Assert
            Assert.That(winner, Is.Null);
        }

        [Test]
        public void Display_ShouldReturnNoEmptyString([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var display = game.Display();

            // Assert
            Assert.That(display, Is.Not.Empty);
        }
    }
}
