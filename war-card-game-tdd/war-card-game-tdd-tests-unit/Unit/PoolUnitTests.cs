using war_card_game_tdd.Enums;
using war_card_game_tdd.Models;
using war_card_game_tdd.Utils;

namespace war_card_game_tdd_tests.Unit
{
    public class PoolUnitTests
    {
        [Test]
        public void Pool_NameEqualToParametrNameCardsIsEmptyAndAddAllRequiredCardsIsNull([Values("Mark", "Ben")] string name)
        {
            // Arrange & Act
            var pool = new Pool(name);
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pool.Name, Is.EqualTo(name));
                Assert.That(pool.Cards, Is.Empty);
                Assert.That(pool.AddAllRequiredCards, Is.Null);
            });
        }

        [Test]
        public void PoolUtils_Create_ShouldReturnEmptyPoolList()
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
        public void PoolUtils_Create_ShouldReturnNoEmptyPoolList([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var players = new List<Player>() 
            { 
                new Player()
                { 
                    Name = name
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
        public void PoolUtils_Clean_AfterCleanShouldReturnEmptyListBecausePoolListIsEmpty()
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
        public void PoolUtils_Clean_AfterCleanShouldReturnNoEmptyListBecausePoolListIsNotEmpty([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var pool = new List<Pool>
            {
                new Pool(name)
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
        public void PoolUtils_Clean_AfterCleanShouldSetAddAllRequiredCardsOnNull([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var pool = new List<Pool>
            {
                new Pool(name)
            };

            // Act
            pool.First()
                .AddAllRequiredCards = true;

            pool.Clean();

            // Assert
            Assert.That(pool.First().AddAllRequiredCards, Is.Null);
        }

        [Test]
        public void PoolUtils_Clean_AfterCleanShouldSetEmptyCardsListForAllPlayer([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var pool = new List<Pool>
            {
                new Pool(name)
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
        public void PoolUtils_GetPoolByPlayerName_ShouldReturnInvalidOperationException([Values("Mark", "Ben")] string name)
        {
            // Arrange
            var pool = new List<Pool>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => pool.GetPoolByPlayerName(name));
        }

        [Test]
        public void PoolUtils_GetPoolByPlayerName_ShouldReturnPoolPlayerByName([Values("Mark", "Ben")] string name)
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
        public void PoolUtils_AddCard_EmptyNameShouldThrowInvalidOperationException([Values("Mark")] string name)
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
        public void PoolUtils_AddCard_AfterAddingEmptyCardToPoolPlayerShouldSetAddAllRequiredCardsOnFalse([Values("Mark")] string name)
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
        public void PoolUtils_AddCard_AfterAddingCardToPoolPlayerShouldSetAddAllRequiredCardsOnTrue([Values("Mark")] string name)
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
        public void PoolUtils_AddCard_AfterAddingCardToPoolPlayerShouldHaveOneCardInPool([Values("Mark")] string name)
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
            Assert.That(poolPlayer.Cards, Has.Count.EqualTo(expected));
        }
    }
}
