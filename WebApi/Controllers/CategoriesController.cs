﻿using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Quaries.GetById;
using Application.Features.Categories.Quaries.GetList;
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Quaries.GetById;
using Application.Features.Products.Quaries.GetList;
using Application.Requests;
using Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            CreatedCategoryResponse response = await Mediator.Send(createCategoryCommand);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCategoryQuery getListCategoryQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListCategoryListItemDto> response = await Mediator.Send(getListCategoryQuery);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            GetByIdCategoryQuery getByIdCategoryQuery = new() { Id = id };
            GetByIdCategoryResponse response = await Mediator.Send(getByIdCategoryQuery);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            UpdatedCategoryResponse response = await Mediator.Send(updateCategoryCommand);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            DeletedCategoryResponse response = await Mediator.Send(new DeleteCategoryCommand { Id = id });
            return Ok(response);
        }
    }
}
