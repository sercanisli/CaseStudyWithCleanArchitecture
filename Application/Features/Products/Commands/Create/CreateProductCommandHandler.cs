using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ProductBusinessRules _productBusinessRules;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ProductBusinessRules productBusinessRules)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<CreatedProductResponse>? Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductTitleCannotBeDuplicatedWhenInserted(request.Title, request.Description);

            Product product = _mapper.Map<Product>(request);
            product.Id = Guid.NewGuid();

            await _productRepository.AddAsync(product);

            CreatedProductResponse createdProductResponse = _mapper.Map<CreatedProductResponse>(product);
            return createdProductResponse;
        }
    }
}
