
using Asp.Versioning;
using BestDigiSellerApp.Stripe.Application.Stripe.Commands.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        return new StatusCodeResult(303);
    }
    [HttpGet("success")]
    [AllowAnonymous]
    public IActionResult Success()
    {
        return Content("The payment has been succesfully taked.");
    }

    [HttpGet("cancel")]
    [AllowAnonymous]
    public IActionResult Cancel()
    {
        return Content("The payment has been canceled.");
    }

}

