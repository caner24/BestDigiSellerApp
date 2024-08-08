using BestDigiSellerApp.Basket.Application.Basket.Commands.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.Basket.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Authorize]
public class BasketController : ControllerBase
{
    private readonly IMediator _mediator;
    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("getBasket")]
    public IActionResult GetBasket()
    {
        return Ok();
    }

    [HttpPost("addBasket")]
    public async Task<IActionResult> AddBasket([FromBody] CreateBasketCommandRequest createBasketCommandRequest)
    {
        var response = await _mediator.Send(createBasketCommandRequest);
        return Ok();
    }

    [HttpPut("updateBasket")]
    public IActionResult UpdateBasket()
    {
        return Ok();
    }

    [HttpDelete("emptyBasket")]
    public IActionResult EmptyBasket()
    {
        return Ok();
    }

}
