using war_card_game_tdd;

namespace war_card_game_tdd_tests.Integration
{
    public class PoolIntegrationTests
    {
        [Test]
        public void Create_ShouldReturnNoEmptyList([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange & Act
            var game = new Game(players);

            // Assert
            Assert.That(players, Is.EqualTo(game.Pool.Count));
        }
    }
}
