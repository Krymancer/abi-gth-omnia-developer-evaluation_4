using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public class GetSalesQuery : IRequest<List<GetSaleResult>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
