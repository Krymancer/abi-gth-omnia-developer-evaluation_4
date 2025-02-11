using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData; // If you're using the optional TestData approach
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    /// <summary>
    /// Unit tests for verifying the discount and quantity rules of SaleItem.
    /// </summary>
    public class SaleItemTests
    {
        /// <summary>
        /// Tests that purchases below 4 items have no discount applied.
        /// </summary>
        [Theory(DisplayName = "Given quantity < 4 When calculating totals Then no discount is applied")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Given_QuantityLessThan4_When_CalculateItemTotals_Then_NoDiscount(int quantity)
        {
            // Arrange
            var saleItem = SaleItemTestData.GenerateItemWithQuantity(quantity);
            var expectedSubtotal = saleItem.UnitPrice * quantity;

            // Act
            saleItem.CalculateItemTotals();

            // Assert
            saleItem.DiscountAmount.Should().Be(0m, because: "discount is not allowed for fewer than 4 items");
            saleItem.TotalAmount.Should().Be(expectedSubtotal, because: "total is simply unitPrice * quantity");
        }

        /// <summary>
        /// Tests that purchases of 4 to 9 items (inclusive) get a 10% discount.
        /// </summary>
        [Theory(DisplayName = "Given 4 <= quantity <= 9 When calculating totals Then 10% discount is applied")]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(9)]
        public void Given_QuantityBetween4And9_When_CalculateItemTotals_Then_10PercentDiscount(int quantity)
        {
            // Arrange
            var saleItem = SaleItemTestData.GenerateItemWithQuantity(quantity);
            var expectedSubtotal = saleItem.UnitPrice * quantity;
            var expectedDiscount = expectedSubtotal * 0.10m;
            var expectedTotal = expectedSubtotal - expectedDiscount;

            // Act
            saleItem.CalculateItemTotals();

            // Assert
            saleItem.DiscountAmount.Should().BeApproximately(expectedDiscount, 0.0001m);
            saleItem.TotalAmount.Should().BeApproximately(expectedTotal, 0.0001m);
        }

        /// <summary>
        /// Tests that purchases of 10 to 20 items (inclusive) get a 20% discount.
        /// </summary>
        [Theory(DisplayName = "Given 10 <= quantity <= 20 When calculating totals Then 20% discount is applied")]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(20)]
        public void Given_QuantityBetween10And20_When_CalculateItemTotals_Then_20PercentDiscount(int quantity)
        {
            // Arrange
            var saleItem = SaleItemTestData.GenerateItemWithQuantity(quantity);
            var expectedSubtotal = saleItem.UnitPrice * quantity;
            var expectedDiscount = expectedSubtotal * 0.20m;
            var expectedTotal = expectedSubtotal - expectedDiscount;

            // Act
            saleItem.CalculateItemTotals();

            // Assert
            saleItem.DiscountAmount.Should().BeApproximately(expectedDiscount, 0.0001m);
            saleItem.TotalAmount.Should().BeApproximately(expectedTotal, 0.0001m);
        }

        /// <summary>
        /// Tests that an exception is thrown if we try selling above 20 identical items.
        /// </summary>
        [Fact(DisplayName = "Given quantity > 20 When calculating totals Then throws DomainException")]
        public void Given_QuantityAbove20_When_CalculateItemTotals_Then_ThrowsDomainException()
        {
            // Arrange
            var saleItem = SaleItemTestData.GenerateItemWithQuantity(21);

            // Act
            Action act = () => saleItem.CalculateItemTotals();

            // Assert
            act.Should().Throw<DomainException>()
               .WithMessage("Cannot sell more than 20 identical items in one sale.*");
        }
    }
}
