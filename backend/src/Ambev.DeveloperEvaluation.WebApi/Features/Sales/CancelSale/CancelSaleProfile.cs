using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    public class CancelSaleProfile : Profile
    {
        public CancelSaleProfile()
        {
            // Map from the WebApi request to the Application layer command
            CreateMap<CancelSaleRequest, CancelSaleCommand>();
        }
    }
}
