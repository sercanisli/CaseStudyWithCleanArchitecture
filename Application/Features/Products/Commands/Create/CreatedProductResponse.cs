namespace Application.Features.Products.Commands.Create
{
    public class CreatedProductResponse
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}