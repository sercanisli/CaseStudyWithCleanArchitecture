using Application.Requests;
using Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Quaries.GetListByCategoryStockLimit
{
    public class GetListByCategoryStockLimitQuery:IRequest<GetListResponse<GetListByCategoryStockLimitListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
        public Guid CategoryId { get; set; }
    }
}
