using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand>
    {
        private readonly ISaleRepository _saleRepository;

        public CancelSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with Id {request.SaleId} not found.");

            if (sale.IsCancelled)
               throw new DomainException("Sale already canceled.");

            sale.Cancel();

            await _saleRepository.UpdateAsync(sale, cancellationToken);
        }
    }
}
