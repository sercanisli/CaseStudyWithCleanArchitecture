using Application.Features.Products.Constants;
using Application.Services.Exceptions.Types;
using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Features.Products.Rules
{
    public class ProductBusinessRules:BaseBusinessRules
    {
        private readonly IProductRepository _productRepository;

        public ProductBusinessRules(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ProductTitleCannotBeDuplicatedWhenInserted(string title, string description)
        {
            Product? result = await _productRepository.GetAsync(predicate:p=>p.Title.ToLower()==title.ToLower() && p.Description.ToLower()==description.ToLower());

            if(result != null)
            {
                throw new BusinessException(ProductsMessages.ProductTitleExists);
            }
        }
    }
}
