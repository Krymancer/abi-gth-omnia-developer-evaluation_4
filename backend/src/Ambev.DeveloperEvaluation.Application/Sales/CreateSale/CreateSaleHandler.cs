using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            // Optionally validate with CreateSaleValidator using pipeline or code below.

            var sale = _mapper.Map<Sale>(request);

            // Domain logic: calculate totals, validate discount logic
            sale.CalculateSaleTotal();

            // save
            await _saleRepository.CreateAsync(sale, cancellationToken);

            // return Sale ID
            return sale.Id;
        }
    }
}
