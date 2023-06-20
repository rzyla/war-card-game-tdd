using war_card_game_tdd.Enums;
using war_card_game_tdd.Models;
using war_card_game_tdd.Utils;

namespace war_card_game_tdd_tests.Unit
{
    public class PlayerUnitTests
    {
        [Test]
        public void Player_CardsDiscardAndNameAreEmpty()
        {
            // Arrange
            var player = new Player();

            // Act
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(player.Name, Is.Empty);
                Assert.That(player.Cards, Has.Count.Zero);
                Assert.That(player.Discard, Has.Count.Zero);
            });
        }

        [Test]
        public void Player_GetCard_PlayerHasNoCardsShouldReturnNull()
        {
            // Arrange
            var player = new Player();

            // Act
            var card = player.GetCard();

            // Assert
            Assert.That(card, Is.Null);
        }

        [Test]
        public void Player_GetCard_AfterAddingCardPlayerHaveOlnyOneCardShouldReturnAddedCard()
        {
            // Arrange
            var player = new Player();
            var newCard = new Card(Color.Diamond, Value.Ace);

            // Act
            player.Cards.Add(newCard);
            var card = player.GetCard();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(newCard.Color, Is.EqualTo(card?.Color));
                Assert.That(newCard.Value, Is.EqualTo(card?.Value));
            });
        }

        [Test]
        public void Player_GetCard_AfterAddingAndRetrievingCardPlayerShouldHaveNoCards()
        {
            // Arrange
            var player = new Player();
            var card = new Card(Color.Diamond, Value.Ace);

            // Act
            player.Cards.Add(card);
            player.GetCard();

            // Assert
            Assert.That(player.Cards, Has.Count.Zero);
        }

        [Test]
        public void Player_AddToCards_AfterAddingPlayerShouldHaveOneCardInCards()
        {
            // Arrange
            var expected = 1;
            var player = new Player();
            var card = new Card(Color.Diamond, Value.Ace);

            // Act
            player.AddToCards(card);

            // Assert
            Assert.That(player.Cards, Has.Count.EqualTo(expected));
        }

        [Test]
        public void Player_AddToDiscard_AfterAddingPlayerShouldHaveOneCardInDiscard()
        {
            // Arrange
            var expected = 1;
            var player = new Player();
            var cards = new List<Card>() { new Card(Color.Diamond, Value.Ace) };

            // Act
            player.AddToDiscard(cards);

            // Assert
            Assert.That(player.Discard, Has.Count.EqualTo(expected));
        }

        [Test]
        public void Player_AddDiscardToCards_AfterTransferDiscardToCardsInCardsShouldBeOneCardAndDiscardShouldBeEmpty()
        {
            // Arrange
            var expected = 1;
            var player = new Player
            {
                Discard = new List<Card>() { new Card(Color.Diamond, Value.Ace) }
            };

            // Act
            player.AddDiscardToCards();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(player.Cards, Has.Count.EqualTo(expected));
                Assert.That(player.Discard, Has.Count.Zero);
            });
        }

        [Test]
        public void PlayerUtils_Create_ShouldReturnEmptyListOfPlayers([Values(-1, 0)] int players)
        {
            // Arrange & Act
            var playersList = PlayerUtils.Create(players);

            // Assert
            Assert.That(playersList, Has.Count.Zero);
        }

        [Test]
        public void PlayerUtils_Create_ShouldReturnListOfPlayers([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange & Act
            var playersList = PlayerUtils.Create(players);

            // Assert
            Assert.That(playersList, Has.Count.EqualTo(players));
        }

        [Test]
        public void PlayerUtils_Create_ShouldReturnListOfPlayersWithoutNameAndCards([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange & Act
            var playersList = PlayerUtils.Create(players);
            var names = playersList.Where(s => !string.IsNullOrEmpty(s.Name)).Count();
            var cards = playersList.Where(s => s.Cards.Any()).Count();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(names, Is.Zero);
                Assert.That(cards, Is.Zero);
            });
        }

        [Test]
        public void PlayerUtils_Create_ShouldReturnListOfPlayersWithEmptyDiscard([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange & Act
            var playersList = PlayerUtils.Create(players);
            var discards = playersList.Select(s => s.Discard.Count).Sum();

            // Assert
            Assert.That(discards, Is.Zero);
        }

        [Test]
        public void PlayerUtils_Create_Names_ShouldReturnListOfPlayersWithNameAndWithoutCards([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange & Act
            var playersList = PlayerUtils.Create(players).Names();
            var names = playersList.Where(s => string.IsNullOrEmpty(s.Name)).Count();
            var cards = playersList.Where(s => s.Cards.Any()).Count();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(names, Is.Zero);
                Assert.That(cards, Is.Zero);
            });
        }

        [Test]
        public void PlayerUtils_Create_Names_ShouldReturnListOfPlayersWithUniqueNames([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange & Act
            var playersList = PlayerUtils.Create(players).Names();
            var names = playersList.Select(s => s.Name).Distinct().Count();

            // Assert
            Assert.That(names, Is.EqualTo(players));
        }

        [Test]
        public void PlayerUtils_Validate_NumberOfPlayersMinus1ZeroOneSevenShouldReturnFalse([Values(-1, 0, 1, 7)] int players)
        {
            // Arrange & Act
            var validate = PlayerUtils.ValidateNumberOfPlayers(players);

            // Assert
            Assert.That(validate, Is.False);
        }

        [Test]
        public void PlayerUtils_Validate_NumberOfPlayers2To6ShouldReturnTrue([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange & Act
            var validate = PlayerUtils.ValidateNumberOfPlayers(players);

            // Assert
            Assert.That(validate, Is.True);
        }

        [Test]
        public void PlayerUtils_GetPlayerByName_ShouldReturnInvalidOperationException([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var players = new List<Player>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => players.GetPlayerByName(name));
        }

        [Test]
        public void PlayerUtils_GetPlayerByName_ShouldReturnPlayerByName([Values("Mark", "Ben")] string name)
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
