using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Quaries.GetById
{
    public class GetByIdProductQuery:IRequest<GetByIdProductResponse>
    {
        public Guid Id { get; set; }
    }
}
