namespace Application.Features.Products.Commands.Create
{
    public class CreatedProductResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
    }
}