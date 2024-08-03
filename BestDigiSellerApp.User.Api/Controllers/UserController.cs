using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Queries.Request;
using BestDigiSellerApp.User.Entity.Dto;
using MassTransit;
using MassTransit.Mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.User.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly MediatR.IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;
    public UserController(MediatR.IMediator mediator, IPublishEndpoint publishEndpoint)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest loginUserCommandRequest)
    {
        var response = await _mediator.Send(loginUserCommandRequest);
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest registerUserCommandRequest)
    {
        var response = await _mediator.Send(registerUserCommandRequest);
        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new ConfirmMailQueryRequest { Token = response.ConfirmationToken, Email = response.Email }, Request.Scheme);
        await _publishEndpoint.Publish<EmailConfirmationDto>(new EmailConfirmationDto { ConfirmationLink = confirmationLink });
        return StatusCode(201);
    }


    [HttpPost("reSendConfirmationToken")]
    public async Task<IActionResult> ReSendConfirmationToken([FromBody] ReConfirmMailCodeCommandRequest resendConfirmationEmailRequest)
    {
        var response = await _mediator.Send(resendConfirmationEmailRequest);
        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { response.Email, response.Token, }, Request.Scheme);
        await _publishEndpoint.Publish<EmailConfirmationDto>(new EmailConfirmationDto { ConfirmationLink = confirmationLink });
        return Ok();
    }

    [HttpGet("confirmMail/{Email}/{Token}")]
    public async Task<IActionResult> ConfirmEmail([FromRoute] ConfirmMailQueryRequest confirmMailQueryRequest)
    {
        var response = await _mediator.Send(confirmMailQueryRequest);
        await _publishEndpoint.Publish<WalletRequest>(new WalletRequest { Currency = Currency.TRY, UserId = confirmMailQueryRequest.Email });

        return Content($"Your email isConfirm -> {response}");
    }

    [HttpPost("loginTwoStep")]
    public async Task<IActionResult> LoginTwoStep([FromBody] LoginTwoStepCommandRequest loginTwoStepCommandRequest)
    {
        var response = await _mediator.Send(loginTwoStepCommandRequest);
        return Ok(response);
    }
}
