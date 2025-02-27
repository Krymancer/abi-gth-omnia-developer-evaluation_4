using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Infrastructure.Messaging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand>
    {
        private readonly ISaleRepository _saleRepository;

        private readonly EventPublisher _eventPublisher;

        public CancelSaleHandler(ISaleRepository saleRepository, EventPublisher eventPublisher)
        {
            _saleRepository = saleRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with Id {request.SaleId} not found.");

            if (sale.IsCancelled)
               throw new DomainException("Sale already canceled.");

            sale.Cancel();

            await _eventPublisher.PublishEvent(sale, "SaleCancelled", cancellationToken);

            await _saleRepository.UpdateAsync(sale, cancellationToken);
        }
    }
}
