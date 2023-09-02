using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommand:IRequest<CreatedProductResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductResponse>
        {
            public Task<CreatedProductResponse>? Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                CreatedProductResponse createdProductResponse = new();
                createdProductResponse.Id = new Guid();
                createdProductResponse.Title = request.Title;
                createdProductResponse.Description= request.Description;
                createdProductResponse.StockQuantity= request.StockQuantity;
                return null;

            }
        }
    }
}
