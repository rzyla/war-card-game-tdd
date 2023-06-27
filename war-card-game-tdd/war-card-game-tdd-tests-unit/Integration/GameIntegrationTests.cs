using war_card_game_tdd;
using war_card_game_tdd.Enums;
using war_card_game_tdd.Models;
using war_card_game_tdd.Utils;

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
        public void Game_GetCardsFromPlayers_PlayerShouldHaveFewerCards([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var _first = game.Players.First();
            var _count = _first.Cards.Count;

            game.GetCardsFromPlayers();

            var first = game.Players.First();
            var count = first.Cards.Count;

            // Assert
            Assert.That(count, Is.Not.EqualTo(_count));
        }

        [Test]
        public void Game_GetCardsFromPlayers_IterationShouldBeGreater([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            game.GetCardsFromPlayers();

            // Assert
            Assert.That(game.Iteration, Is.Not.Zero);
        }

        [Test]
        public void Game_IfPlayerHasntCardAddDiscardToCardsAndShuffle_DiscardShouldBeEmptyCardsEqualMultiplicationOfColorAndValue([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);
            var colors = Enum.GetValues(typeof(Color));
            var values = Enum.GetValues(typeof(Value));
            var expectedElements = colors.Length * values.Length;

            // Act
            foreach (var player in game.Players)
            {
                player.Discard = player.Cards;
                player.Cards = new List<Card>();
            }

            game.IfPlayerHasntCardAddDiscardToCardsAndShuffle();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(game.Players.Sum(s => s.Discard.Count), Is.Zero);
                Assert.That(game.Players.Sum(s => s.Cards.Count), Is.EqualTo(expectedElements));
            });
        }

        [Test]
        public void Game_IfPlayerHasntCardSetAddAllRequiredCardsFalse_AddAllRequiredCardsIsEqualToNumberOfPlayers([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            game.Players.ForEach(f => f.Cards = new List<Card>());
            game.IfPlayerHasntCardSetAddAllRequiredCardsFalse();

            var count = game.Pool.Where(w => w.AddAllRequiredCards == false).Count();

            // Assert
            Assert.That(players, Is.EqualTo(count));
        }

        [Test]
        public void Game_IfPlayerHasntCardSetAddAllRequiredCardsFalse_AddAllRequiredCardsIsNotEqualToNumberOfPlayers([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            game.Players.ForEach(f => f.Cards = new List<Card>());

            var first = game.Players.First();
            first.Cards.Add(new Card(Color.Spade, Value.King));

            game.IfPlayerHasntCardSetAddAllRequiredCardsFalse();

            var count = game.Pool.Where(w => w.AddAllRequiredCards == false).Count();

            // Assert
            Assert.That(players, Is.Not.EqualTo(count));
        }

        [Test]
        public void Game_ComparePool_ShouldReturnFalse([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            game.Players.ForEach(f => f.Cards = new List<Card>());

            var first = game.Players.First();
            first.Cards.Add(new Card(Color.Spade, Value.King));

            var last = game.Players.Last();
            last.Cards.Add(new Card(Color.Heart, Value.King));

            game.Pool.AddCard(first.Name, first.GetCard());
            game.Pool.AddCard(last.Name, last.GetCard());

            var compare = game.ComparePool();

            // Assert
            Assert.That(compare, Is.False);
        }

        [Test]
        public void Game_ComparePool_ShouldReturnTrue([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            game.Players.ForEach(f => f.Cards = new List<Card>());

            var first = game.Players.First();
            first.Cards.Add(new Card(Color.Spade, Value.King));

            var last = game.Players.Last();
            last.Cards.Add(new Card(Color.Heart, Value.Ace));

            game.Pool.AddCard(first.Name, first.GetCard());
            game.Pool.AddCard(last.Name, last.GetCard());

            var compare = game.ComparePool();

            // Assert
            Assert.That(compare, Is.True);
        }

        [Test]
        public void Game_AddCardsToWinner_IsWinnerPlayerShouldHaveMoreCards([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            game.Players.ForEach(f => f.Cards = new List<Card>());

            var first = game.Players.First();
            first.Cards.Add(new Card(Color.Spade, Value.King));
            first.Cards.Add(new Card(Color.Spade, Value.Two));

            var firstCount = first.Cards.Count;

            var last = game.Players.Last();
            last.Cards.Add(new Card(Color.Heart, Value.Ace));

            game.AddCardsToWinner();

            // Assert
            Assert.That(firstCount, Is.EqualTo(first.Cards.Count));
        }

        [Test]
        public void Game_Winner_PlayersHaveDifferentCardsThereIsWinner([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            game.Players.ForEach(f => f.Cards = new List<Card>());
            
            var first = game.Players.First();
            first.Cards.Add(new Card(Color.Spade, Value.King));
            first.Cards.Add(new Card(Color.Spade, Value.Two));

            var last = game.Players.Last();
            last.Cards.Add(new Card(Color.Heart, Value.Ace));

            game.GetCardsFromPlayers();

            var winnder = game.Winner();

            // Assert
            Assert.That(winnder, Is.Not.Null);
        }

        [Test]
        public void Game_Display_AfterGetCardsFromPlayersDisplayWasChanged([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var _display = game.Display();
            game.GetCardsFromPlayers();
            var display = game.Display();

            // Assert
            Assert.That(display, Is.Not.EqualTo(_display));
        }

        [Test]
        public void Game_Display_AfterAddCardDisplayWasChanged([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var _display = game.Display();
            var first = game.Players.First();
            var card = first.GetCard();
            game.Pool.AddCard(first.Name, card);
            var display = game.Display();

            // Assert
            Assert.That(display, Is.Not.EqualTo(_display));
        }

        [Test]
        public void Game_Display_AfterAddCardsToWinnerDisplayWasChanged([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var game = new Game(players);

            // Act
            var _display = game.Display();
            var first = game.Players.First();
            var card = first.GetCard();
            game.Pool.AddCard(first.Name, card);
            game.AddCardsToWinner();
            var display = game.Display();

            // Assert
            Assert.That(display, Is.Not.EqualTo(_display));
        }
    }
}
