using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Quaries.GetById
{
    public class GetByIdCategoryQuery:IRequest<GetByIdCategoryResponse>
    {
        public Guid Id { get; set; }
    }
}
