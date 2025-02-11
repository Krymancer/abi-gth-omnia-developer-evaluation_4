using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
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

            // Replace Items
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
        }
    }
}
