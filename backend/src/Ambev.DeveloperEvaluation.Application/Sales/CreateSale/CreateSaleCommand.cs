using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a new sale.
    /// </summary>
    public class CreateSaleCommand : IRequest<Guid> // Return the new Sale ID
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public List<CreateSaleItemDto> Items { get; set; } = new();
    }

    public class CreateSaleItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
