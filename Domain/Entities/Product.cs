using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : Entity<Guid>
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }

        public virtual Category? Category { get; set; }
        public Product()
        {
            
        }
        public Product(Guid id, Guid categoryId, string title, string description, int stockQuantity ):this()
        {
            Id = id;
            CategoryId = categoryId;
            Title = title; 
            Description = description; 
            StockQuantity = stockQuantity;
        }
    }
}
