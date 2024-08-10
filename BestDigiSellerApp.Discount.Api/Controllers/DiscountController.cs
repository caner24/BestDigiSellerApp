using Asp.Versioning;
using BestDigiSellerApp.Discount.Api.ActionFilters;
using BestDigiSellerApp.Discount.Application.Discount.Commands.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.Discount.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Authorize]
[ApiVersion("1.0")]
public class DiscountController : ControllerBase
{
    private readonly IMediator _mediator;
    public DiscountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("createCouponCode")]
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCouponCode([FromBody] CreateCouponCodeCommandRequest createCouponCodeCommandRequest)
    {
        var response = await _mediator.Send(createCouponCodeCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        return Ok();
    }

    [HttpPost("validateCouponCodeAsync")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> ValidateCouponCodeAsync([FromBody] ValidateCouponCodeCommandRequest validateCouponCodeCommandRequest)
    {

        var response = await _mediator.Send(validateCouponCodeCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        return Ok(response.Value);
    }

}
