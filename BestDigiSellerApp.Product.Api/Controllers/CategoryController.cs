using Asp.Versioning;
using BestDigiSellerApp.Product.Api.ActionFilters;
using BestDigiSellerApp.Product.Application.Product.Commands.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace BestDigiSellerApp.Product.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    [ApiVersion("1.0")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        private readonly IMediator _mediator;
        public CategoryController(IDistributedCache cache, IMediator mediator)
        {
            _cache = cache;
            _mediator = mediator;
        }

        [HttpPost("createCategory")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProduct(CreateCategoryCommandRequest createCategoryCommandRequest)
        {
            var response = await _mediator.Send(createCategoryCommandRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Errors);

            return Ok(response.Value);
        }


    }
}
