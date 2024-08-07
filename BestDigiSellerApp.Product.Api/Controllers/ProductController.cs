using Asp.Versioning;
using BestDigiSellerApp.Product.Api.ActionFilters;
using BestDigiSellerApp.Product.Application.Product.Commands.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace BestDigiSellerApp.Product.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
[ApiVersion("1.0")]
[Authorize]

public class ProductController : ControllerBase
{
    private readonly IDistributedCache _cache;
    private readonly IMediator _mediator;
    public ProductController(IDistributedCache cache, IMediator mediator)
    {
        _cache = cache;
        _mediator = mediator;
    }



    [HttpPost("createProduct")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateProduct(CreateProductCommandRequest createProductCommandRequest)
    {
        var response = await _mediator.Send(createProductCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        return Ok(response.Value);
    }

}
