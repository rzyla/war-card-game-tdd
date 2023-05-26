using war_card_game_tdd;
using war_card_game_tdd.Enums;
using war_card_game_tdd.Models;
using war_card_game_tdd.Utils;

namespace war_card_game_tdd_tests.Unit
{
    public class PlayerUnitTests
    {
        [Test]
        public void Create_ShouldReturnEmptyElements([Values(-1, 0)] int players)
        {
            // Arrange
            var expected = 0;

            // Act
            var playersList = PlayerUtils.Create(players);

            // Assert
            Assert.That(playersList, Has.Count.EqualTo(expected));
        }

        [Test]
        public void Create_ShouldReturnElements([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange & Act
            var playersList = PlayerUtils.Create(players);

            // Assert
            Assert.That(playersList, Has.Count.EqualTo(players));
        }

        [Test]
        public void Create_ShouldReturnElementsWithoutNameAndCards([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var expected = 0;

            // Act
            var playersList = PlayerUtils.Create(players);
            var names = playersList.Where(s => !string.IsNullOrEmpty(s.Name)).Count();
            var cards = playersList.Where(s => s.Cards.Any()).Count();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(names, Is.EqualTo(expected));
                Assert.That(cards, Is.EqualTo(expected));
            });
        }

        [Test]
        public void Create_ShouldReturnElementsWithoutDiscard([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var expected = 0;

            // Act
            var playersList = PlayerUtils.Create(players);
            var discards = playersList.Select(s => s.Discard.Count).Sum();

            // Assert
            Assert.That(discards, Is.EqualTo(expected));
        }

        [Test]
        public void Create_Names_ShouldReturnElementsWithNameAndWithoutCards([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var expected = 0;

            // Act
            var playersList = PlayerUtils.Create(players).Names();
            var names = playersList.Where(s => string.IsNullOrEmpty(s.Name)).Count();
            var cards = playersList.Where(s => s.Cards.Any()).Count();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(names, Is.EqualTo(expected));
                Assert.That(cards, Is.EqualTo(expected));
            });
        }

        [Test]
        public void Create_Names_ShouldReturnUniqueNames([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange & Act
            var playersList = PlayerUtils.Create(players).Names();
            var names = playersList.Select(s => s.Name).Distinct().Count();

            // Assert
            Assert.That(names, Is.EqualTo(players));
        }

        [Test]
        public void Validate_NumberOfPlayersMinus1ShouldReturnFalse([Values(-1, 0, 1, 7)] int players)
        {
            // Arrange & Act
            var validate = PlayerUtils.ValidateNumberOfPlayers(players);

            // Assert
            Assert.That(validate, Is.False);
        }

        [Test]
        public void Validate_NumberOfPlayers2To6ShouldReturnTrue([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange & Act
            var validate = PlayerUtils.ValidateNumberOfPlayers(players);

            // Assert
            Assert.That(validate, Is.True);
        }

        [Test]
        public void AddToCards_PlayerHasOneItemInCards()
        {
            // Arrange
            var player = new Player();
            var card = new Card(Color.Diamond, Value.Ace);
            var expected = 1;

            // Act
            player.AddToCards(card);

            // Assert
            Assert.That(player.Cards, Has.Count.EqualTo(expected));
        }

        [Test]
        public void GetCard_ReturnNull()
        {
            // Arrange
            var player = new Player();

            // Act
            var card = player.GetCard();

            // Assert
            Assert.That(card, Is.Null);
        }

        [Test]
        public void GetCard_ReturnElement()
        {
            // Arrange
            var player = new Player();

            // Act
            player.AddToCards(new Card(Color.Diamond, Value.Ace));
            var card = player.GetCard();

            // Assert
            Assert.That(card, Is.Not.Null);
        }

        [Test]
        public void GetCard_CardsHasOneElementLess()
        {
            // Arrange
            var player = new Player();

            // Act
            player.AddToCards(new Card(Color.Diamond, Value.Ace));
            player.GetCard();

            // Assert
            Assert.That(player.Cards, Is.Empty);
        }

        [Test]
        public void AddToDiscard_DiscardCountEqualAddElements()
        {
            // Arrange
            var player = new Player();
            var cards = new List<Card>()
            {
                new Card(Color.Diamond, Value.Ace),
                new Card(Color.Diamond, Value.Six)
            };

            // Act
            player.AddToDiscard(cards);

            // Assert
            Assert.That(player.Discard, Has.Count.EqualTo(cards.Count));
        }

        [Test]
        public void AddDiscardToCards_CardsCountEqualAddElements()
        {
            // Arrange
            var expectedDiscard = 0;
            var player = new Player();
            var cards = new List<Card>()
            {
                new Card(Color.Diamond, Value.Ace),
                new Card(Color.Diamond, Value.Six)
            };

            // Act
            player.AddToDiscard(cards);
            player.AddDiscardToCards();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(player.Cards, Has.Count.EqualTo(cards.Count));
                Assert.That(player.Discard, Has.Count.EqualTo(expectedDiscard));
            });
        }

        [Test]
        public void GetPlayerByName_ShouldReturnInvalidOperationException([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var players = new List<Player>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => players.GetPlayerByName(name));
        }

        [Test]
        public void GetPlayerByName_ShouldReturnPlayerByName([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var players = new List<Player>();
            players.Add(new Player() { Name = name });

            // Act
            var player = players.GetPlayerByName(name);

            // Assert
            Assert.That(player.Name, Is.EqualTo(name));
        }
    }
}
