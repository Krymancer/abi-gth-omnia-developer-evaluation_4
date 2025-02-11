using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Infrastructure.Messaging;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly EventPublisher _eventPublisher;

        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, EventPublisher eventPublisher)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
        }

        public async Task Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with Id {request.SaleId} not found.");

            if (sale.IsCancelled)
                throw new DomainException("Cannot update a canceled sale.");

            // Update basic info
            sale.SaleNumber = request.SaleNumber;
            sale.SaleDate = request.SaleDate;

            var cancelledItems = sale.Items.Where(i => !request.Items.Any(ri => ri.ProductId == i.ProductId)).ToList();
            
            sale.Items.Clear();
            foreach (var itemDto in request.Items)
            {
                sale.Items.Add(new SaleItem
                {
                    ProductId = itemDto.ProductId,
                    ProductName = itemDto.ProductName,
                    UnitPrice = itemDto.UnitPrice,
                    Quantity = itemDto.Quantity
                });
            }

            // Recalc
            sale.CalculateSaleTotal();

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            await _eventPublisher.PublishEvent(sale, "SaleUpdated", cancellationToken);
            cancelledItems.ForEach(async i => await _eventPublisher.PublishEvent(i, "ItemCancelled", cancellationToken));

        }
    }
}
