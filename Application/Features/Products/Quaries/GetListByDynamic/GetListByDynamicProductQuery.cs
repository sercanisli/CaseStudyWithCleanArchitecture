using Application.Requests;
using Application.Responses;
using Application.Services.Dynamic;
using MediatR;

namespace Application.Features.Products.Quaries.GetListByDynamic
{
    public class GetListByDynamicProductQuery:IRequest<GetListResponse<GetListByDynamicProductListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
        public DynamicQuery DynamicQuery { get; set; }
    }
}
