using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales
{
    public class GetSalesProfile : Profile
    {
        public GetSalesProfile()
        {
            // If you have a request object, map it to the query
            CreateMap<GetSalesRequest, GetSalesQuery>();

            // The "ListSales" query in Application returns a List<GetSaleResult>
            // so we just re-use the same "GetSaleResult" classes for each item

            CreateMap<GetSaleResult, GetSalesResponse>()
                .ForMember(d => d.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<GetSaleItemResult, GetSalesItemResponse>();
        }
    }
}
