using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Quaries.GetById;
using Application.Features.Products.Quaries.GetList;
using Application.Features.Products.Quaries.GetListByCategoryStockLimit;
using Application.Features.Products.Quaries.GetListByDynamic;
using Application.Features.Products.Quaries.GetListProducstHasNotStock;
using Application.Responses;
using Application.Services.Paging;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Products.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, CreatedProductResponse>().ReverseMap();

            CreateMap<Product, UpdateProductCommand>().ReverseMap();
            CreateMap<Product, UpdatedProductResponse>().ReverseMap();

            CreateMap<Product, DeleteProductCommand>().ReverseMap();
            CreateMap<Product, DeletedProductResponse>().ReverseMap();

            CreateMap<Product, GetListProductListItemDto>()
                .ForMember(destinationMember: p => p.CategoryName, memberOptions: opt => opt.MapFrom(p => p.Category.Name))
                .ReverseMap();
            CreateMap<Paginate<Product>, GetListResponse<GetListProductListItemDto>>().ReverseMap();

            CreateMap<Product, GetListByDynamicProductListItemDto>()
                .ForMember(destinationMember: p => p.CategoryName, memberOptions: opt => opt.MapFrom(p => p.Category.Name))
                .ReverseMap();
            CreateMap<Paginate<Product>, GetListResponse<GetListByDynamicProductListItemDto>>().ReverseMap();

            CreateMap<Product, GetListByStockQuantityListItemDto>()
                .ForMember(destinationMember: p => p.CategoryName, memberOptions: opt => opt.MapFrom(p => p.Category.Name))
                .ReverseMap();
            CreateMap<Paginate<Product>, GetListResponse<GetListByStockQuantityListItemDto>>().ReverseMap();

            CreateMap<Product, GetListByCategoryStockLimitListItemDto>()
                .ForMember(destinationMember: p => p.CategoryName, memberOptions: opt => opt.MapFrom(p => p.Category.Name))
                .ReverseMap();
            CreateMap<Paginate<Product>, GetListResponse<GetListByCategoryStockLimitListItemDto>>().ReverseMap();



            CreateMap<Product, GetByIdProductResponse>().ReverseMap();
        }
    }
}
