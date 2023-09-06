using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Quaries.GetById;
using Application.Features.Products.Quaries.GetList;
using Application.Features.Products.Quaries.GetListByCategoryStockLimit;
using Application.Features.Products.Quaries.GetListByDynamic;
using Application.Features.Products.Quaries.GetListProducstHasNotStock;
using Application.Requests;
using Application.Responses;
using Application.Services.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CreateProductCommand createProductCommand)
        {
            CreatedProductResponse response = await Mediator.Send(createProductCommand);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProductQuery getListProductQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListProductListItemDto> response = await Mediator.Send(getListProductQuery);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            GetByIdProductQuery getByIdProductQuery = new() { Id = id };
            GetByIdProductResponse response = await Mediator.Send(getByIdProductQuery);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand updateProductCommand)
        {
            UpdatedProductResponse response = await Mediator.Send(updateProductCommand);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            DeletedProductResponse response = await Mediator.Send(new DeleteProductCommand { Id = id });
            return Ok(response);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody]DynamicQuery? dynamicQuery)
        {
            GetListByDynamicProductQuery getListByDynamicProductQuery = new() { PageRequest = pageRequest, DynamicQuery=dynamicQuery };
            GetListResponse<GetListByDynamicProductListItemDto> response = await Mediator.Send(getListByDynamicProductQuery);
            return Ok(response);
        }
        [HttpPost("GetList/ByStockQuantity")]
        public async Task<IActionResult> GetListByStockQuantity([FromQuery] PageRequest pageRequest, [FromQuery] int stockQuantity)
        {
            GetListByStockQuantityQuery getListProductsHasNotStockQuery = new() { PageRequest = pageRequest, StockQuantity=stockQuantity };
            GetListResponse<GetListByStockQuantityListItemDto> response = await Mediator.Send(getListProductsHasNotStockQuery);
            return Ok(response);
        }
        [HttpPost("GetList/ByCategoryStockLimit")]
        public async Task<IActionResult> GetListByCategoryStockLimit([FromQuery] PageRequest pageRequest, [FromQuery] Guid categoryId)
        {
            GetListByCategoryStockLimitQuery getListByCategoryStockLimitQuery = new() { PageRequest = pageRequest, CategoryId=categoryId };
            GetListResponse<GetListByCategoryStockLimitListItemDto> response = await Mediator.Send(getListByCategoryStockLimitQuery);
            return Ok(response);
        }
    }
}
