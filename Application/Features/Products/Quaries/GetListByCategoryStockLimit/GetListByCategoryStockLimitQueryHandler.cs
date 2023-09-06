using Application.Responses;
using Application.Services.Paging;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Quaries.GetListByCategoryStockLimit
{
    public class GetListByCategoryStockLimitQueryHandler : IRequestHandler<GetListByCategoryStockLimitQuery, GetListResponse<GetListByCategoryStockLimitListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetListByCategoryStockLimitQueryHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByCategoryStockLimitListItemDto>> Handle(GetListByCategoryStockLimitQuery request, CancellationToken cancellationToken)
        {
            Category? category = await _categoryRepository.GetAsync(predicate: c => c.Id == request.CategoryId, cancellationToken: cancellationToken);
            var limit = category.StockLimit;

            Paginate<Product> products = await _productRepository.GetListAsync(
                predicate: p => p.StockQuantity >=limit && p.CategoryId==request.CategoryId, include: c => c.Include(c => c.Category),
                index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize, cancellationToken: cancellationToken
                );

            GetListResponse<GetListByCategoryStockLimitListItemDto> response = _mapper.Map<GetListResponse<GetListByCategoryStockLimitListItemDto>>(products);
            return response;
        }
    }
}
