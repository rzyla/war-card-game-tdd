using war_card_game_tdd.Enums;
using war_card_game_tdd.Models;
using war_card_game_tdd.Utils;

namespace war_card_game_tdd_tests.Unit
{
    public class PoolUnitTests
    {
        [Test]
        public void Create_ShouldReturnEmptyList()
        {
            // Arrange
            var players = new List<Player>();

            // Act
            var pool = PoolUtils.Create(players);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pool, Is.Not.Null);
                Assert.That(pool, Is.Empty);
            });
        }

        [Test]
        public void Create_ShouldReturnNoEmptyList()
        {
            // Arrange
            var players = new List<Player>() 
            { 
                new Player()
                { 
                    Name = "Ben"
                }
            };

            // Act
            var pool = PoolUtils.Create(players);
            var poolPlayer = pool.First();
            var player = players.First();

            // Assert
            Assert.That(poolPlayer.Name, Is.EqualTo(player.Name));
        }

        [Test]
        public void Clean_ShouldReturnEmptyListBecausePoolListIsEmpty()
        {
            // Arrange
            var pool = new List<Pool>();

            // Act
            pool.Clean();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pool, Is.Not.Null);
                Assert.That(pool, Is.Empty);
            });
        }

        [Test]
        public void Clean_ShouldReturnNoEmptyListBecausePoolListIsNotEmpty()
        {
            // Arrange
            var pool = new List<Pool>
            {
                new Pool("Ben")
            };

            // Act
            pool.Clean();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pool, Is.Not.Null);
                Assert.That(pool, Is.Not.Empty);
            });
        }

        [Test]
        public void Clean_ShouldReturnAddAllRequiredCardsNull()
        {
            // Arrange
            var pool = new List<Pool>
            {
                new Pool("Ben")
            };

            // Act
            pool.First()
                .AddAllRequiredCards = true;

            pool.Clean();

            // Assert
            Assert.That(pool.First().AddAllRequiredCards, Is.Null);
        }

        [Test]
        public void Clean_ShouldReturnEmptyCardsList()
        {
            // Arrange
            var pool = new List<Pool>
            {
                new Pool("Ben")
            };
            var cards = new List<Card>()
            {
                new Card(Color.Spade, Value.Ace)
            };

            // Act
            pool.First()
                .Cards = cards;

            pool.Clean();

            var first = pool.First();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(first.Cards, Is.Not.Null);
                Assert.That(first.Cards, Is.Empty);
            });
        }

        [Test]
        public void GetPoolByPlayerName_ShouldReturnInvalidOperationException([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var pool = new List<Pool>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => pool.GetPoolByPlayerName(name));
        }

        [Test]
        public void GetPoolByPlayerName_ShouldReturnPlayerByName([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var pool = new List<Pool>
            {
                new Pool(name)
            };

            // Act
            var player = pool.GetPoolByPlayerName(name);

            // Assert
            Assert.That(player.Name, Is.EqualTo(name));
        }

        [Test]
        public void AddCard_EmptyNaneShouldThrowInvalidOperationException([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var pool = new List<Pool>
            {
                new Pool(name)
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => pool.AddCard(string.Empty, null));
        }

        [Test]
        public void AddCard_EmptyCardShouldSetAddAllRequiredCardsFalse([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var pool = new List<Pool>
            {
                new Pool(name)
            };

            // Act
            pool.AddCard(name, null);
            var poolPlayer = pool.GetPoolByPlayerName(name);

            // Assert
            Assert.That(poolPlayer.AddAllRequiredCards, Is.False);
        }

        [Test]
        public void AddCard_ShouldSetAddAllRequiredCardsTrue([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var pool = new List<Pool>
            {
                new Pool(name)
            };
            var card = new Card(Color.Spade, Value.Ace);

            // Act
            pool.AddCard(name, card);
            var poolPlayer = pool.GetPoolByPlayerName(name);

            // Assert
            Assert.That(poolPlayer.AddAllRequiredCards, Is.True);
        }

        [Test]
        public void AddCard_2([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var expected = 1;
            var pool = new List<Pool>
            {
                new Pool(name)
            };
            var card = new Card(Color.Spade, Value.Ace);

            // Act
            pool.AddCard(name, card);
            var poolPlayer = pool.GetPoolByPlayerName(name);

            // Assert
            Assert.AreEqual(expected, poolPlayer.Cards.Count);
        }
    }
}
