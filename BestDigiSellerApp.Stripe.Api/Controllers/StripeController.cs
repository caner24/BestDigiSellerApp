
using Asp.Versioning;
using BestDigiSellerApp.Stripe.Application.Stripe.Commands.Request;
using BestDigiSellerApp.Stripe.Entity.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace BestDigiSellerApp.Stripe.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/")]
[ApiVersion("1.0")]

public class StripeController : ControllerBase
{
    private readonly IMediator _mediator;
    public StripeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("createCheckoutSessionForProduct")]
    public async Task<IActionResult> CreateCheckoutSessionForProduct([FromBody] CreateCheckoutSessionForProductCommandRequest createCheckoutSessionForProductCommandRequest)
    {
        var response = await _mediator.Send(createCheckoutSessionForProductCommandRequest);
        HttpContext.Response.Headers.Add("Location", response.Value);
        return Ok(response.Value);
    }
    [HttpGet("success")]
    [AllowAnonymous]
    public async Task<IActionResult> Success([FromQuery] CreateSuccessBillingCommandRequest createSuccessBillingCommandRequest)
    {
        var response = await _mediator.Send(createSuccessBillingCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        return Ok();
    }

    [HttpGet("cancel")]
    [AllowAnonymous]
    public IActionResult Cancel()
    {
        return Content("The payment has been canceled.");
    }

}

