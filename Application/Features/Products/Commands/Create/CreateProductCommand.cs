using MediatR;

namespace Application.Features.Products.Commands.Create
{
    public partial class CreateProductCommand:IRequest<CreatedProductResponse>
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
    }
}
