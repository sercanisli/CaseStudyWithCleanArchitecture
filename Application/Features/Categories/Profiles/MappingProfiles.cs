using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Quaries.GetById;
using Application.Features.Categories.Quaries.GetList;
using Application.Responses;
using Application.Services.Paging;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Categories.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, CreatedCategoryResponse>().ReverseMap();

            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
            CreateMap<Category, UpdatedCategoryResponse>().ReverseMap();

            CreateMap<Category, DeleteCategoryCommand>().ReverseMap();
            CreateMap<Category, DeletedCategoryResponse>().ReverseMap();

            CreateMap<Category, GetListCategoryListItemDto>().ReverseMap();
            CreateMap<Paginate<Category>, GetListResponse<GetListCategoryListItemDto>>().ReverseMap();

            CreateMap<Category, GetByIdCategoryResponse>().ReverseMap();
        }
    }
}
