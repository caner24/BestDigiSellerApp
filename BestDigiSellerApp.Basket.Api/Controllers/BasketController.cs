using BestDigiSellerApp.Basket.Api.ActionFilters;
using BestDigiSellerApp.Basket.Application.Basket.Commands.Request;
using BestDigiSellerApp.Basket.Application.Basket.Queries.Request;
using FluentResults;
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
    public async Task<IActionResult> GetBasket()
    {
        var request = new GetUserBasketQueryRequest();
        var response = await _mediator.Send(request);
        if (response is null)
            return BadRequest(response.Errors);

        return Ok(response.Value);
    }

    [HttpPost("addBasket")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> AddBasket([FromBody] CreateBasketCommandRequest createBasketCommandRequest)
    {
        var response = await _mediator.Send(createBasketCommandRequest);
        if (response.IsFailed)
            return BadRequest(response.Errors);

        return Ok();
    }

    [HttpDelete("emptyBasket")]
    public async Task<IActionResult> EmptyBasket()
    {
        var request = new EmptyBasketCommandRequest();
        var response = await _mediator.Send(request);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);
        return Ok();
    }

    [HttpPut("deleteItemForBasket")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> DeleteItemForBasket([FromBody] DeleteItemToBasketCommandRequest deleteItemToBasketCommandRequest)
    {
        var response = await _mediator.Send(deleteItemToBasketCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);


        return Ok();
    }

    [HttpPost("createCheckoutSessionForProduct")]
    public async Task<IActionResult> CreateCheckoutSessionForBasket([FromBody] CreateCheckoutSessionBasketCommandRequest createCheckoutSessionBasketCommandRequest)
    {
        var response = await _mediator.Send(createCheckoutSessionBasketCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);
        return Ok(response.Value);
    }

}
