using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleQuery : IRequest<GetSaleResult>
    {
        public Guid SaleId { get; set; }

        public GetSaleQuery(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
