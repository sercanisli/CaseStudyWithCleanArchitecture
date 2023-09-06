using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommand:IRequest<UpdatedCategoryResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StockLimit { get; set; }

    }
}
