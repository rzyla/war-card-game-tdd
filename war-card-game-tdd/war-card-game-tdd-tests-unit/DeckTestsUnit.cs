using war_card_game_tdd;
using war_card_game_tdd.Models;
using war_card_game_tdd.Enums;

namespace war_card_game_tdd_tests_unit
{
    public class DeckTestsUnit
    {
        [Test]
        public void New_DeckShouldReturnElementsEqualsMultiplicationOfColorAndValue()
        {
            // Arrange
            var deck = new Deck();
            var colors = Enum.GetValues(typeof(Color));
            var values = Enum.GetValues(typeof(Value));
            var expectedElements = colors.Length * values.Length;

            // Act
            var cards = deck.New();

            // Assert
            Assert.That(cards, Has.Count.EqualTo(expectedElements));
        }

        [Test]
        public void New_DeckShouldReturnElementsOnlyInEnumsColors()
        {
            // Arrange
            var deck = new Deck();
            var expectedColors = Enum.GetValues(typeof(Color)).Length;

            // Act
            var cards = deck.New();

            // Assert
            Assert.That(cards.GroupBy(g => g.Color).Count(), Is.EqualTo(expectedColors));
        }

        [Test]
        public void New_DeckShouldReturnElementsOnlyInEnumsValues()
        {
            // Arrange
            var deck = new Deck();
            var expectedValues = Enum.GetValues(typeof(Value)).Length;

            // Act
            var cards = deck.New();

            // Assert
            Assert.That(cards.GroupBy(g => g.Value).Count(), Is.EqualTo(expectedValues));
        }

        [Test]
        public void New_DeckShouldReturnFirstElementValueEqualToFirstEnumValue()
        {
            // Arrange
            var deck = new Deck();
            var values = Enum.GetValues(typeof(Value));
            var expectedValue = values.GetValue(0);

            // Act
            var cards = deck.New();

            // Assert
            Assert.That(cards.First().Value, Is.EqualTo(expectedValue));
        }

        [Test]
        public void New_DeckShouldReturnFirstElementValueEqualToFirstEnumColor()
        {
            // Arrange
            var deck = new Deck();
            var colors = Enum.GetValues(typeof(Color));
            var expectedColor = colors.GetValue(0);

            // Act
            var cards = deck.New();

            // Assert
            Assert.That(cards.First().Color, Is.EqualTo(expectedColor));
        }

        [Test]
        public void New_DeckShouldReturnLastElementValueEqualToLastEnumValue()
        {
            // Arrange
            var deck = new Deck();
            var values = Enum.GetValues(typeof(Value));
            var expectedValue = values.GetValue(values.Length - 1);

            // Act
            var cards = deck.New();

            // Assert
            Assert.That(cards.Last().Value, Is.EqualTo(expectedValue));
        }

        [Test]
        public void New_DeckShouldReturnLastElementColorEqualToLastEnumColor()
        {
            // Arrange
            var deck = new Deck();
            var colors = Enum.GetValues(typeof(Color));
            var expectedColor = colors.GetValue(colors.Length - 1);

            // Act
            var cards = deck.New();

            // Assert
            Assert.That(cards.Last().Color, Is.EqualTo(expectedColor));
        }

        [Test]
        public void Get_DeckShouldReturn0Elements()
        {
            // Arrange
            var deck = new Deck();
            var expectedElements = 0;

            // Act
            var cards = deck.Get();

            // Assert
            Assert.That(cards.Count, Is.EqualTo(expectedElements));
        }

        [Test]
        public void Get_DeckShouldReturnElementsEqualsMultiplicationOfColorAndValue()
        {
            // Arrange
            var deck = new Deck();
            var colors = Enum.GetValues(typeof(Color));
            var values = Enum.GetValues(typeof(Value));
            var expectedElements = colors.Length * values.Length;

            // Act
            deck.New();
            var cards = deck.Get();

            // Assert
            Assert.That(cards.Count, Is.EqualTo(expectedElements));
        }

        [Test]
        public void Shuffle_DeckShouldReturnElementsEqualsMultiplicationOfColorAndValue()
        {
            // Arrange
            var deck = new Deck();
            var colors = Enum.GetValues(typeof(Color));
            var values = Enum.GetValues(typeof(Value));
            var expectedElements = colors.Length * values.Length;

            // Act
            deck.New();
            var cards = deck.Shuffle();

            // Assert
            Assert.That(cards.Count, Is.EqualTo(expectedElements));
        }

        [Test]
        public void Shuffle_ShouldReturnDifferentDeck()
        {
            // Arrange
            var cards = new Deck()
                .New();
            var deck = new Deck();
            var equal = 0;

            // Act
            deck.New();
            var shuffled = deck.Shuffle();

            for(var i = 0; i < cards.Count; i++)
            {
                if (shuffled[i].Color == cards[i].Color && shuffled[i].Value == cards[i].Value)
                {
                    equal++;
                }
            }

            Assert.That(equal, Is.Not.EqualTo(cards.Count));
        }
    }
}