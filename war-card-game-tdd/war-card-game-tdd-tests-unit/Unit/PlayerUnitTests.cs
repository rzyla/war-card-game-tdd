using war_card_game_tdd.Utils;

namespace war_card_game_tdd_tests.Unit
{
    public class PlayerUnitTests
    {
        [Test]
        public void Create_ShouldReturn4Elements()
        {
            // Arrange
            var createPlayers = 4;
            var expectedElements = 4;

            // Act
            var players = PlayerUtils.Create(createPlayers);

            // Assert
            Assert.That(players, Has.Count.EqualTo(expectedElements));
        }

        [Test]
        public void Create_ShouldReturnElementsWithoutNameAndCards()
        {
            // Arrange
            var createPlayers = 2;
            var expectedElements = 0;

            // Act
            var players = PlayerUtils.Create(createPlayers);
            var names = players.Where(s => !string.IsNullOrEmpty(s.Name)).Count();
            var cards = players.Where(s => s.Cards.Any()).Count();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(names, Is.EqualTo(expectedElements));
                Assert.That(cards, Is.EqualTo(expectedElements));
            });
        }

        [Test]
        public void Create_ShouldReturnElementsWithoutDiscard()
        {
            // Arrange
            var createPlayers = 2;
            var expectedElements = 0;

            // Act
            var players = PlayerUtils.Create(createPlayers);
            var discards = players.Select(s => s.Discard.Count).Sum();

            // Assert
            Assert.That(discards, Is.EqualTo(expectedElements));
        }

        [Test]
        public void Create_Names_ShouldReturnElementsWithNameAndWithoutCards()
        {
            // Arrange
            var createPlayers = 3;
            var expectedElements = 0;

            // Act
            var players = PlayerUtils.Create(createPlayers).Names();
            var names = players.Where(s => string.IsNullOrEmpty(s.Name)).Count();
            var cards = players.Where(s => s.Cards.Any()).Count();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(names, Is.EqualTo(expectedElements));
                Assert.That(cards, Is.EqualTo(expectedElements));
            });
        }

        [Test]
        public void Create_Names_ShouldReturnUniqueNames()
        {
            // Arrange
            var createPlayers = 4;
            var expectedElements = 4;

            // Act
            var players = PlayerUtils.Create(createPlayers).Names();
            var names = players.Select(s => s.Name).Distinct().Count();

            // Assert
            Assert.That(names, Is.EqualTo(expectedElements));
        }

        [Test]
        public void Validate_NumberOfPlayersMinus1ShouldReturnFalse([Values(-1, 0, 1, 7)] int players)
        {
            // Arrange
            // Act
            var validate = PlayerUtils.ValidateNumberOfPlayers(players);

            // Assert
            Assert.That(validate, Is.False);
        }


        [Test]
        public void Validate_NumberOfPlayers2To6ShouldReturnTrue([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            // Act
            var validate = PlayerUtils.ValidateNumberOfPlayers(players);

            // Assert
            Assert.That(validate, Is.True);
        }
    }
}
