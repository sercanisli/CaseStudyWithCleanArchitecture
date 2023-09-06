using Application.Features.Products.Quaries.GetList;
using Application.Responses;
using Application.Services.Paging;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Quaries.GetListByDynamic
{
    public class GetListByDynamicProductQueryHandler:IRequestHandler<GetListByDynamicProductQuery,GetListResponse<GetListByDynamicProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListByDynamicProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicProductListItemDto>> Handle(GetListByDynamicProductQuery request, CancellationToken cancellationToken)
        {
            Paginate<Product> products = await _productRepository
               .GetListByDynamicAsync(request.DynamicQuery,
                include: c => c.Include(c => c.Category), 
                index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize, cancellationToken: cancellationToken);
            GetListResponse<GetListByDynamicProductListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicProductListItemDto>>(products);
            return response;
        }
    }
}
