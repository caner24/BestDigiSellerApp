using Asp.Versioning;
using BestDigiSellerApp.Product.Api.ActionFilters;
using BestDigiSellerApp.Product.Application.Product.Commands.Request;
using BestDigiSellerApp.Product.Application.Product.Queries.Request;
using BestDigiSellerApp.Product.Entity.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BestDigiSellerApp.Product.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    [ApiVersion("1.0")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private LinkGenerator _linkGenerator;
        public CategoryController(IMediator mediator, LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
            _mediator = mediator;
        }

        [HttpGet("getAllCategories")]
        [OutputCache(Duration = 10)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoryQueryRequest getAllCategoryQueryRequest)
        {
            var response = await _mediator.Send(getAllCategoryQueryRequest);
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
                var ownerLinks = CreateLinksForCategories(response.Value[index].Id, getAllCategoryQueryRequest.Fields);
                shapedProducts[index].Add("Links", ownerLinks);
            }
            var productsWrapper = new LinkCollectionWrapper<BestDigiSellerApp.Product.Entity.Entity>(shapedProducts);
            return Ok(CreateLinksForCategories(productsWrapper));
        }

        [HttpGet("getCategoriesById/{Id}")]
        [OutputCache(Duration = 10)]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoriesById([FromRoute] GetCategoryByIdQueryRequest getProductByIdQueryRequest)
        {
            var response = await _mediator.Send(getProductByIdQueryRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Errors);
            return Ok(response.Value);
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

        [HttpDelete("deleteCategory/{Id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteCategory([FromRoute] DeleteCategoryCommandRequest deleteCategoryCommandRequest)
        {
            var response = await _mediator.Send(deleteCategoryCommandRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Errors);

            return Ok();
        }

        [HttpPut("UpdateCategory")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommandRequest updateCategoryCommandRequest)
        {
            var response = await _mediator.Send(updateCategoryCommandRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Errors);

            return Ok();
        }

        private IEnumerable<Link> CreateLinksForCategories(Guid id, string fields = "")
        {
            var links = new List<Link>
            {
        new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetCategoriesById), values: new { id, fields }),
                "self",
                "GET"),
        new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteCategory), values: new { id }),
                "delete_product",
                "DELETE"),
        new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdateCategory), values: new { id }),
        "update_product",
        "PUT")
    };
            return links;
        }
        private LinkCollectionWrapper<BestDigiSellerApp.Product.Entity.Entity> CreateLinksForCategories(LinkCollectionWrapper<BestDigiSellerApp.Product.Entity.Entity> productsWrapper)
        {
            productsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetAllCategories), values: new { }),
                    "self",
                    "GET"));
            return productsWrapper;
        }


    }
}
