using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace BestDigiSellerApp.Product.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
[ApiVersion("1.0")]
[Authorize(Roles = "Admin")]

public class ProductController : ControllerBase
{
    private readonly IDistributedCache _cache;
    public ProductController(IDistributedCache cache)
    {
        _cache = cache;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetAllProduct()
    {

        return Ok();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetAllProductById()
    {

        return Ok();
    }

    [HttpPost]
    public IActionResult CreateProduct()
    {

        return Ok();
    }
    [HttpDelete]
    public IActionResult DeleteProduct()
    {

        return Ok();
    }
    [HttpPut]
    public IActionResult UpdateProduct()
    {

        return Ok();
    }
}
