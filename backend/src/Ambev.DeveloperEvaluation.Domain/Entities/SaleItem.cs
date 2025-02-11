using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an individual product item in a sale, 
    /// including quantity, price, discount, etc.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Foreign key to the parent sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// External identity of the Product.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Denormalized name of the Product.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Unit price for this product (might be denormalized from an external pricing system).
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Quantity of this product in the sale.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The discount amount for this item (calculated).
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// The final total amount for this item, after discounts.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Applies business rules for discount tiers and calculates final amounts.
        /// </summary>
        public void CalculateItemTotals()
        {
            // Max 20 items
            if (Quantity > 20)
            {
                throw new DomainException("Cannot sell more than 20 identical items in one sale.");
            }

            // Calculate discount tier
            decimal discountPercentage = 0;
            if (Quantity >= 10 && Quantity <= 20)
            {
                discountPercentage = 0.20m;
            }
            else if (Quantity >= 4)
            {
                discountPercentage = 0.10m;
            }
            else
            {
                discountPercentage = 0;
            }

            DiscountAmount = (UnitPrice * Quantity) * discountPercentage;
            TotalAmount = (UnitPrice * Quantity) - DiscountAmount;
        }
    }
}
