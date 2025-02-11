using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<Sale, GetSaleResult>()
                .ForMember(d => d.Items, opt => opt.MapFrom(s => s.Items));

            CreateMap<SaleItem, GetSaleItemResult>();

        }
    }
}
