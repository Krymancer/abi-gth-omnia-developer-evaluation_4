using Ambev.DeveloperEvaluation.Domain.Common;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction in the system.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// A unique sale number (like an invoice number).
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Date/time when the sale was made.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// External identity for the customer (denormalized).
        /// For example, CustomerId from another system.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Customer name, denormalized from an external domain.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// External identity for the branch.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Branch name, denormalized from an external domain.
        /// </summary>
        public string BranchName { get; set; } = string.Empty;

        /// <summary>
        /// The total amount of the sale (summation of item totals).
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Indicates if the sale is canceled (cannot be edited).
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Collection of items in this sale.
        /// </summary>
        public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

        /// <summary>
        /// Recalculate the total for the entire Sale based on its Items.
        /// </summary>
        public void CalculateSaleTotal()
        {
            decimal sum = 0;
            foreach (var item in Items)
            {
                item.CalculateItemTotals(); // see SaleItem
                sum += item.TotalAmount;
            }
            TotalAmount = sum;
        }

        /// <summary>
        /// Marks the sale as canceled (soft-delete).
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
        }
    }
}
