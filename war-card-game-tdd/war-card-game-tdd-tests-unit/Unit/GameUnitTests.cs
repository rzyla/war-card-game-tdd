using war_card_game_tdd;
using war_card_game_tdd.Enums;
using war_card_game_tdd.Models;

namespace war_card_game_tdd_tests.Unit
{
    public class GameUnitTests
    {
        [Test]
        public void Game_Iteration_PropertyShouldBeEqualZero([Values(2, 3)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            // Assert
            Assert.That(game.Iteration, Is.EqualTo(0));
        }

        [Test]
        public void Game_Iteration_PropertyShouldBeGreaterThanZero([Values(2, 3)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            game.GetCardsFromPlayers();

            // Assert
            Assert.That(game.Iteration, Is.GreaterThan(0));
        }

        [Test]
        public void Game_Values_PropertyShouldHaveAllEnumValues([Values(2, 3)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var count = game.Values.Count();
            var length = Enum.GetValues(typeof(Value)).Length;

            // Assert
            Assert.That(count, Is.EqualTo(length));
        }

        [Test]
        public void Game_Values_PropertyShouldHaveFirstElementAce([Values(2, 3)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var first = game.Values.First();

            // Assert
            Assert.That(first, Is.EqualTo(Value.Ace));
        }

        [Test]
        public void Game_Values_PropertyShouldHaveLastElementIsTwo([Values(2, 3)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var last = game.Values.Last();

            // Assert
            Assert.That(last, Is.EqualTo(Value.Two));
        }

        [Test]
        public void Game_GetCardsFromPlayers_ShouldReturnApplicationException([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);
            game.Players = new List<Player>();

            // Act & Assert
            Assert.Throws<ApplicationException>(() => game.GetCardsFromPlayers());
        }

        [Test]
        public void Game_IfPlayerHasntCardAddDiscardToCardsAndShuffle_([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);
            var first = game.Players.First();
            first.Discard = first.Cards;
            first.Cards = new List<Card>();

            // Act
            game.IfPlayerHasntCardAddDiscardToCardsAndShuffle();

            first = game.Players.First();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(first.Cards, Is.Not.Empty);
                Assert.That(first.Discard, Is.Empty);
            });
        }

        [Test]
        public void Game_IfPlayerHasntCardSetAddAllRequiredCardsFalse_AllPlayersHaveCardsShouldDontSetAddAllRequiredCardsFalse([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            game.IfPlayerHasntCardSetAddAllRequiredCardsFalse();

            // Assert
            Assert.That(game.Pool.Where(w => w.AddAllRequiredCards == false).Count, Is.EqualTo(0));
        }

        [Test]
        public void Game_IfPlayerHasntCardSetAddAllRequiredCardsFalse_OnePlayersHasntCardsShouldSetAddAllRequiredCardsFalse([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            game.Players.First().Cards = new List<Card>();
            game.IfPlayerHasntCardSetAddAllRequiredCardsFalse();

            // Assert
            Assert.That(game.Pool.Where(w => w.AddAllRequiredCards == false).Count, Is.EqualTo(1));
        }

        [Test]
        public void Game_IfPlayerHasntCardSetAddAllRequiredCardsFalse_AllPlayersHasntCardsShouldSetAddAllRequiredCardsFalse([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            game.Players.ForEach(e => e.Cards = new List<Card>());
            game.IfPlayerHasntCardSetAddAllRequiredCardsFalse();

            // Assert
            Assert.That(game.Pool.Where(w => w.AddAllRequiredCards == false).Count, Is.EqualTo(players));
        }

        [Test]
        public void Game_ComparePool_CardsInPoolIsEmptyShouldReturnFalse([Values(2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var comparePool = game.ComparePool();

            // Assert
            Assert.That(comparePool, Is.False);
        }

        [Test]
        public void Game_ComparePool_AddAllRequiredCardsEqualsTrueButInPoolAreNotCardsShouldReturnFalse([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var first = game.Pool.First();
            first.AddAllRequiredCards = true;

            var comparePool = game.ComparePool();

            // Assert
            Assert.That(comparePool, Is.False);
        }

        [Test]
        public void Game_ComparePool_InPoolIsOneCardButAddAllRequiredCardsIsNullShouldReturnFalse([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var card = new Card(Color.Diamond, Value.Ace);

            var first = game.Pool.First();
            first.Cards.Add(card);

            var comparePool = game.ComparePool();

            // Assert
            Assert.That(comparePool, Is.False);
        }

        [Test]
        public void Game_ComparePool_InPoolIsOneCardButAddAllRequiredCardsIsFalseShouldReturnFalse([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var card = new Card(Color.Diamond, Value.Ace);

            var first = game.Pool.First();
            first.AddAllRequiredCards = false;
            first.Cards.Add(card);

            var comparePool = game.ComparePool();

            // Assert
            Assert.That(comparePool, Is.False);
        }

        [Test]
        public void Game_ComparePool_InPoolIsOneCardButAddAllRequiredCardsIsFalseShouldReturnTrue([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var card = new Card(Color.Diamond, Value.Ace);

            var first = game.Pool.First();
            first.AddAllRequiredCards = true;
            first.Cards.Add(card);

            var comparePool = game.ComparePool();

            // Assert
            Assert.That(comparePool, Is.True);
        }

        [Test]
        public void Game_ComparePool_InPoolAreThisSameCardsValueAndIsNotWinnerShouldReturnFalse([Values(2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var card = new Card(Color.Diamond, Value.Ace);

            foreach (var pool in game.Pool)
            {
                pool.AddAllRequiredCards = true;
                pool.Cards.Add(card);
            }

            var comparePool = game.ComparePool();

            // Assert
            Assert.That(comparePool, Is.False);
        }

        [Test]
        public void Game_ComparePool_InPoolAreThisSameCardsValueButAddAllRequiredCardsInAllPlayersHaveFalseShouldReturnFalse([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var card = new Card(Color.Diamond, Value.Ace);

            foreach (var pool in game.Pool)
            {
                pool.AddAllRequiredCards = false;
                pool.Cards.Add(card);
            }

            var comparePool = game.ComparePool();

            // Assert
            Assert.That(comparePool, Is.False);
        }

        [Test]
        public void Game_ComparePool_InPoolAreThisSameCardsValueButAddAllRequiredCardsInOnePlayerIsFalseShouldReturnTrue([Values(2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var card = new Card(Color.Diamond, Value.Ace);

            foreach(var pool in game.Pool)
            {
                pool.AddAllRequiredCards = false;
                pool.Cards.Add(card);
            }

            var last = game.Pool.Last();
            last.AddAllRequiredCards = true;
            last.Cards.Add(card);

            var comparePool = game.ComparePool();

            // Assert
            Assert.That(comparePool, Is.True);
        }

        [Test]
        public void Game_AddCardsToWinner_NoCardsInPoolDiscardCountShouldBeEqualZero([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);
            var player = game.Players.First();

            // Act
            var cards = new List<Card>();

            game.Pool.Where(w => w.Name.Equals(player.Name)).First().Cards = cards;
            game.AddCardsToWinner();

            // Assert
            Assert.That(player.Discard, Has.Count.EqualTo(0));
        }

        [Test]
        public void Game_AddCardsToWinner_PoolHaveWinnerButAddAllRequiredCardsIsNullDiscardCountShouldBeEqualZero([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);
            var player = game.Players.First();

            // Act
            var cards = new List<Card>()
            {
                new Card(Color.Spade,Value.Ace)
            };

            var pool = game.Pool.Where(w => w.Name.Equals(player.Name)).First();
            pool.Cards = cards;

            game.AddCardsToWinner();

            // Assert
            Assert.That(player.Discard, Has.Count.EqualTo(0));
        }

        [Test]
        public void Game_AddCardsToWinner_PoolHaveWinnerButAddAllRequiredCardsIsFalseDiscardCountShouldBeEqualZero([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);
            var player = game.Players.First();

            // Act
            var cards = new List<Card>()
            {
                new Card(Color.Spade,Value.Ace)
            };

            var pool = game.Pool.Where(w => w.Name.Equals(player.Name)).First();
            pool.Cards = cards;
            pool.AddAllRequiredCards = false;

            game.AddCardsToWinner();

            // Assert
            Assert.That(player.Discard, Has.Count.EqualTo(0));
        }

        [Test]
        public void Game_AddCardsToWinner_PoolHaveWinnerShouldAddCardsToDiscardPlayer([Values(1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);
            var player = game.Players.First();

            // Act
            var cards = new List<Card>()
            {
                new Card(Color.Spade,Value.Ace)
            };

            var pool = game.Pool.Where(w => w.Name.Equals(player.Name)).First();
            pool.Cards = cards;
            pool.AddAllRequiredCards = true;

            game.AddCardsToWinner();

            // Assert
            Assert.That(player.Discard, Has.Count.EqualTo(cards.Count));
        }

        [Test]
        public void Game_Winner_PlayersListIsEmptyOrAllPlayersHaveCardsShouldReturnEmptyElement([Values(-1, 0, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var winner = game.Winner();

            // Assert
            Assert.That(winner, Is.Null);
        }

        [Test]
        public void Game_Winner_AllPlayersHaveCardsShouldReturnEmptyElement([Values(-1, 0, 1, 2, 3, 4, 5, 6, 7)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            game.Players.ForEach(f => f.Cards = new List<Card>());
            var winner = game.Winner();

            // Assert
            Assert.That(winner, Is.Null);
        }

        [Test]
        public void Game_Winner_PlayerHaveCardsOnDiscardShouldReturnEmptyElement([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var cards = new List<Card>()
            {
                new Card(Color.Spade, Value.Ace)
            };

            game.Players.ForEach(f => f.Cards = new List<Card>());
            game.Players.First().Cards = cards;
            game.Players.Last().Discard = cards;
            var winner = game.Winner();

            // Assert
            Assert.That(winner, Is.Null);
        }

        [Test]
        public void Game_Winner_ShouldReturnNotNullElement([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var cards = new List<Card>()
            {
                new Card(Color.Spade, Value.Ace)
            };

            game.Players.ForEach(f => f.Cards = new List<Card>());
            game.Players.First().Cards = cards;
            var winner = game.Winner();

            // Assert
            Assert.That(winner, Is.Not.Null);
        }

        [Test]
        public void Game_Display_ShouldReturnEmptyString([Values(-1, 0)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var display = game.Display();

            // Assert
            Assert.That(display, Is.Empty);
        }

        [Test]
        public void Game_Display_ShouldReturnNoEmptyString([Values(1, 2, 3, 4, 5, 6, 7)] int players)
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
