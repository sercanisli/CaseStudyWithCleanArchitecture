using Application.Requests;
using Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Quaries.GetList
{
    public class GetListCategoryQuery:IRequest<GetListResponse<GetListCategoryListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
    }
}
