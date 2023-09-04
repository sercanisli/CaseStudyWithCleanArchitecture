using Application.Requests;
using Application.Responses;
using MediatR;

namespace Application.Features.Products.Quaries.GetList
{
    public class GetListProductQuery:IRequest<GetListResponse<GetListProductListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
    }
}
