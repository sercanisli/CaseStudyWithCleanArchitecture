using Application.Features.Products.Quaries.GetList;
using Application.Responses;
using Application.Services.Paging;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Quaries.GetListProducstHasNotStock
{
    public class GetListByStockQuantityQueryHandler:IRequestHandler<GetListByStockQuantityQuery,GetListResponse<GetListByStockQuantityListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListByStockQuantityQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<GetListResponse<GetListByStockQuantityListItemDto>> Handle(GetListByStockQuantityQuery request, CancellationToken cancellationToken)
        {
            Paginate<Product> products = await _productRepository.GetListAsync(
                predicate: p => p.StockQuantity >= request.StockQuantity, include: c => c.Include(c => c.Category),
                index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize, cancellationToken: cancellationToken
                );

            GetListResponse<GetListByStockQuantityListItemDto> response = _mapper.Map<GetListResponse<GetListByStockQuantityListItemDto>>(products);
            return response;
        }
    }
}
