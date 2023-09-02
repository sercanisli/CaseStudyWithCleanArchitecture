using Application.Features.Products.Commands.Create;
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
    }
}
