namespace Application.Features.Products.Quaries.GetList
{
    public class GetListProductListItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
    }
}
