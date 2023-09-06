using Application.Requests;
using Application.Responses;
using MediatR;

namespace Application.Features.Products.Quaries.GetListProducstHasNotStock
{
    public class GetListByStockQuantityQuery:IRequest<GetListResponse<GetListByStockQuantityListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
        public int StockQuantity { get; set; }
    }
}
