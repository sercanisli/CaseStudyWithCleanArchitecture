using Application.Services.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ProductRepository : EntityFrameworkRepositoryBase<Product, Guid, BaseDbContext>, IProductRepository
    {
        public ProductRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
