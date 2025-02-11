using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SaleItemTestData
    {
        private static readonly Faker<SaleItem> SaleItemFaker =
            new Faker<SaleItem>()
                .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(5, 100))    // Random price between 5 and 100
                .RuleFor(i => i.Quantity, f => f.Random.Int(1, 30))         // Random quantity between 1 and 30
                .RuleFor(i => i.DiscountAmount, 0m)
                .RuleFor(i => i.TotalAmount, 0m);

        /// <summary>
        /// Generates a random sale item that might break or satisfy different discount rules.
        /// </summary>
        public static SaleItem GenerateRandomItem()
        {
            return SaleItemFaker.Generate();
        }

        /// <summary>
        /// Generates a sale item with the specified quantity and a random unit price.
        /// </summary>
        public static SaleItem GenerateItemWithQuantity(int quantity)
        {
            var item = SaleItemFaker.Generate();
            item.Quantity = quantity;
            return item;
        }
    }
}
