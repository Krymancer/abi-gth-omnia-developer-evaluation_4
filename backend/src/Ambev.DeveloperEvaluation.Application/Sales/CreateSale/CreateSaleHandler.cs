using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Infrastructure.Messaging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        private readonly EventPublisher _eventPublisher;

        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, EventPublisher eventPublisher)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            // Optionally validate with CreateSaleValidator using pipeline or code below.

            var sale = _mapper.Map<Sale>(request);

            // Domain logic: calculate totals, validate discount logic
            sale.CalculateSaleTotal();

            // save
            await _saleRepository.CreateAsync(sale, cancellationToken);

            await _eventPublisher.PublishEvent(sale, "SaleCreated", cancellationToken);

            // return Sale ID
            return sale.Id;
        }
    }
}
