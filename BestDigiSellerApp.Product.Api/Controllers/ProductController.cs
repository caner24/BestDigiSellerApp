using Asp.Versioning;
using BestDigiSellerApp.Product.Api.ActionFilters;
using BestDigiSellerApp.Product.Application.Product.Commands.Request;
using BestDigiSellerApp.Product.Application.Product.Queries.Request;
using BestDigiSellerApp.Product.Entity;
using BestDigiSellerApp.Product.Entity.Helpers;
using Google.Protobuf.WellKnownTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BestDigiSellerApp.Product.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
[ApiVersion("1.0")]
[Authorize(Roles = "Admin")]

public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private LinkGenerator _linkGenerator;
    public ProductController(LinkGenerator linkGenerator, IMediator mediator)
    {
        _linkGenerator = linkGenerator;
        _mediator = mediator;
    }


    [HttpGet("getAllProducts")]
    [OutputCache(Duration = 10)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductQueryRequest gellAllProductQueryRequest)
    {
        var response = await _mediator.Send(gellAllProductQueryRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        var metadata = new
        {
            response.Value.TotalCount,
            response.Value.PageSize,
            response.Value.CurrentPage,
            response.Value.TotalPages,
            response.Value.HasNext,
            response.Value.HasPrevious
        };

        var shapedProducts = response.Value.Select(o => o.Entity).ToList();
        Response.Headers.TryAdd("X-Pagination", JsonConvert.SerializeObject(metadata));
        for (var index = 0; index < response.Value.Count(); index++)
        {
            var ownerLinks = CreateLinksForProduct(response.Value[index].Id, gellAllProductQueryRequest.Fields);
            shapedProducts[index].Add("Links", ownerLinks);
        }
        var productsWrapper = new LinkCollectionWrapper<BestDigiSellerApp.Product.Entity.Entity>(shapedProducts);
        return Ok(CreateLinksForProduct(productsWrapper));
    }

    [HttpGet("getProductById/{ProductId}")]
    [OutputCache(Duration = 10)]
    [AllowAnonymous]
    public async Task<IActionResult> GetProductById([FromRoute] GetProductByIdQueryRequest getProductByIdQueryRequest)
    {
        var response = await _mediator.Send(getProductByIdQueryRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);
        return Ok(response.Value);
    }


    [HttpPost("createProduct")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommandRequest createProductCommandRequest)
    {
        var response = await _mediator.Send(createProductCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        return Ok(response.Value);
    }

    [HttpDelete("deleteProduct/{ProductId}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
    {
        var response = await _mediator.Send(deleteProductCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        return Ok();
    }

    [HttpPut("updateProduct")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest createProductCommandRequest)
    {
        var response = await _mediator.Send(createProductCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        return Ok();
    }

    private IEnumerable<Link> CreateLinksForProduct(Guid id, string fields = "")
    {
        var links = new List<Link>
        {
        new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetProductById), values: new { ProductId =id }),
            "self",
            "GET"),
        new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteProduct), values: new { ProductId =id }),
            "delete_product",
            "DELETE"),
        new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdateProduct), values: new {  }),
        "update_product",
        "PUT")
    };
        return links;
    }
    private LinkCollectionWrapper<BestDigiSellerApp.Product.Entity.Entity> CreateLinksForProduct(LinkCollectionWrapper<BestDigiSellerApp.Product.Entity.Entity> productsWrapper)
    {
        productsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetAllProducts), values: new { }),
                "self",
                "GET"));
        return productsWrapper;
    }

}
