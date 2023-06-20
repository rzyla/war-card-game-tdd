using war_card_game_tdd.Enums;
using war_card_game_tdd.Utils;

namespace war_card_game_tdd_tests.Unit
{
    public class DeckUnitTests
    {
        [Test]
        public void DeckUtils_New_ReturnedElementsShouldEqualsMultiplicationOfColorAndValue()
        {
            // Arrange
            var colors = Enum.GetValues(typeof(Color));
            var values = Enum.GetValues(typeof(Value));
            var expectedElements = colors.Length * values.Length;

            // Act
            var cards = DeckUtils.New();

            // Assert
            Assert.That(cards, Has.Count.EqualTo(expectedElements));
        }

        [Test]
        public void DeckUtils_New_ReturnedElementsShouldOnlyInEnumsColors()
        {
            // Arrange
            var expectedColors = Enum.GetValues(typeof(Color)).Length;

            // Act
            var cards = DeckUtils.New();

            // Assert
            Assert.That(cards.GroupBy(g => g.Color).Count(), Is.EqualTo(expectedColors));
        }

        [Test]
        public void DeckUtils_New_ReturnedElementsShouldOnlyInEnumsValues()
        {
            // Arrange
            var expectedValues = Enum.GetValues(typeof(Value)).Length;

            // Act
            var cards = DeckUtils.New();

            // Assert
            Assert.That(cards.GroupBy(g => g.Value).Count(), Is.EqualTo(expectedValues));
        }

        [Test]
        public void DeckUtils_New_ReturnedFirstElementShouldValueEqualToFirstEnumValue()
        {
            // Arrange
            var values = Enum.GetValues(typeof(Value));
            var expectedValue = values.GetValue(0);

            // Act
            var cards = DeckUtils.New();

            // Assert
            Assert.That(cards.First().Value, Is.EqualTo(expectedValue));
        }

        [Test]
        public void DeckUtils_New_ReturnedFirstElementShouldValueEqualToFirstEnumColor()
        {
            // Arrange
            var colors = Enum.GetValues(typeof(Color));
            var expectedColor = colors.GetValue(0);

            // Act
            var cards = DeckUtils.New();

            // Assert
            Assert.That(cards.First().Color, Is.EqualTo(expectedColor));
        }

        [Test]
        public void DeckUtils_New_ReturnedLastElementShouldValueEqualToLastEnumValue()
        {
            // Arrange
            var values = Enum.GetValues(typeof(Value));
            var expectedValue = values.GetValue(values.Length - 1);

            // Act
            var cards = DeckUtils.New();

            // Assert
            Assert.That(cards.Last().Value, Is.EqualTo(expectedValue));
        }

        [Test]
        public void DeckUtils_New_ReturnedLastElementShouldColorEqualToLastEnumColor()
        {
            // Arrange
            var colors = Enum.GetValues(typeof(Color));
            var expectedColor = colors.GetValue(colors.Length - 1);

            // Act
            var cards = DeckUtils.New();

            // Assert
            Assert.That(cards.Last().Color, Is.EqualTo(expectedColor));
        }

        [Test]
        public void DeckUtils_Shuffle_ReturnedElementsShouldEqualsMultiplicationOfColorAndValue()
        {
            // Arrange
            var colors = Enum.GetValues(typeof(Color));
            var values = Enum.GetValues(typeof(Value));
            var expectedElements = colors.Length * values.Length;

            // Act
            var cards = DeckUtils.New()
                .Shuffle();

            // Assert
            Assert.That(cards, Has.Count.EqualTo(expectedElements));
        }

        [Test]
        public void DeckUtils_Shuffle_ReturnedDeckShouldBeDifferent()
        {
            // Arrange
            var equal = 0;

            // Act
            var cardsToCompare = DeckUtils.New();
            var shuffled = DeckUtils.New()
                .Shuffle();

            for (var i = 0; i < cardsToCompare.Count; i++)
            {
                if (shuffled[i].Color == cardsToCompare[i].Color && shuffled[i].Value == cardsToCompare[i].Value)
                {
                    equal++;
                }
            }

            // Assert
            Assert.That(equal, Is.Not.EqualTo(cardsToCompare.Count));
        }

        [Test]
        public void DeckUtils_Split_ShouldReturnExpectedPayers([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange & Act
            var playersList = PlayerUtils.Create(players);
            DeckUtils.New()
                .Shuffle()
                .Split(playersList);

            // Assert
            Assert.That(playersList, Has.Count.EqualTo(players));
        }

        [Test]
        public void DeckUtils_Split_ReturnedElementsShouldEqualsMultiplicationOfColorAndValue([Values(2, 3, 4, 5, 6)] int players)
        {
            // Arrange
            var colors = Enum.GetValues(typeof(Color));
            var values = Enum.GetValues(typeof(Value));
            var expectedElements = colors.Length * values.Length;

            // Act
            var playersList = PlayerUtils.Create(players);
            DeckUtils.New()
                .Shuffle()
                .Split(playersList);

            var cards = playersList.Select(s => s.Cards.Count).Sum();

            // Assert
            Assert.That(cards, Is.EqualTo(expectedElements));
        }

        [Test]
        public void DeckUtils_Split_ReturnedElementsShouldEqualsMultiplicationOfColorAndValueDivisonPlayers([Values(2, 4)] int players)
        {
            // Arrange
            var colors = Enum.GetValues(typeof(Color));
            var values = Enum.GetValues(typeof(Value));
            var shuffledCards = colors.Length * values.Length / players;

            // Act
            var playersList = PlayerUtils.Create(players);
            DeckUtils.New()
                .Shuffle()
                .Split(playersList);

            var notEquals = playersList.Where(s => !s.Cards.Count.Equals(shuffledCards)).Any();

            // Assert
            Assert.That(notEquals, Is.False);
        }
    }
}