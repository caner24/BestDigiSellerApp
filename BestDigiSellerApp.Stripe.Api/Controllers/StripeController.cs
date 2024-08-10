
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
    public IActionResult Success([FromQuery] string session_id)
    {
        var service = new SessionService();
        try
        {
            Session session = service.Get(session_id);

            var products = new List<ProductDto>();

            for (int i = 0; i < session.Metadata.Count / 2; i++)
            {
                var productId = session.Metadata[$"productid_{i}"];
                var quantity = session.Metadata[$"quantity_{i}"];
                products.Add(new ProductDto
                {
                    ProductId = productId,
                    Quantity = int.Parse(quantity)
                });
            }
            return Ok();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("cancel")]
    [AllowAnonymous]
    public IActionResult Cancel()
    {
        return Content("The payment has been canceled.");
    }

}

