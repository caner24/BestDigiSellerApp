using Asp.Versioning;
using BestDigiSellerApp.User.Api.ActionFilters;
using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Queries.Request;
using BestDigiSellerApp.User.Entity.Dto;
using MassTransit;
using MassTransit.Mediator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.User.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
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
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest loginUserCommandRequest)
    {
        var response = await _mediator.Send(loginUserCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        return Ok(response.Value);
    }

    [HttpPost("register")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest registerUserCommandRequest)
    {
        var response = await _mediator.Send(registerUserCommandRequest);
        if (response.IsFailed)
            return StatusCode(StatusCodes.Status422UnprocessableEntity, response.Errors);

        var confirmationLink = Url.Action(nameof(ConfirmEmail), "User", new ConfirmMailQueryRequest { Token = response.Value.ConfirmationToken, Email = response.Value.Email }, Request.Scheme);
        await _publishEndpoint.Publish<EmailConfirmationDto>(new EmailConfirmationDto { ConfirmationLink = confirmationLink, EmailAdress = registerUserCommandRequest.Email });
        return StatusCode(201);
    }


    [HttpPost("reSendConfirmationToken")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> ReSendConfirmationToken([FromBody] ReConfirmMailCodeCommandRequest resendConfirmationEmailRequest)
    {
        var response = await _mediator.Send(resendConfirmationEmailRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        var confirmationLink = Url.Action(nameof(ConfirmEmail), "User", new { response.Value.Email, response.Value.Token, }, Request.Scheme);
        await _publishEndpoint.Publish<EmailConfirmationDto>(new EmailConfirmationDto { ConfirmationLink = confirmationLink, EmailAdress = resendConfirmationEmailRequest.EmailAdress });
        return Ok();
    }

    [HttpPost("forgottenPassword")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> ForgottenPassword([FromBody] ForgottenPasswordCommandRequest forgottenPasswordCommandRequest)
    {
        var response = await _mediator.Send(forgottenPasswordCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        await _publishEndpoint.Publish<ForgottonPasswordDto>(new ForgottonPasswordDto { Email = forgottenPasswordCommandRequest.Email, Token = response.Value.Token });
        return Ok();
    }

    [HttpGet("confirmMail/{Email}/{Token}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> ConfirmEmail([FromRoute] ConfirmMailQueryRequest confirmMailQueryRequest)
    {
        var response = await _mediator.Send(confirmMailQueryRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);
        await _publishEndpoint.Publish<WalletRequestDto>(new WalletRequestDto { Currency = 2, UserEmail = confirmMailQueryRequest.Email });
        return Content($"Your email isConfirmed");
    }

    [HttpPost("resetPassword")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> ResetPassword([FromBody] PasswordResetCommandRequest passwordResetCommandRequest)
    {
        var response = await _mediator.Send(passwordResetCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        return Ok();
    }

    [HttpPost("loginTwoStep")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> LoginTwoStep([FromBody] LoginTwoStepCommandRequest loginTwoStepCommandRequest)
    {
        var response = await _mediator.Send(loginTwoStepCommandRequest);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        return Ok(response.Value);
    }


    [HttpPost("manage2factor")]
    [Authorize]
    public async Task<IActionResult> Manage2factor()
    {
        var request = new TwoFactorEnableRequest();
        var response = await _mediator.Send(request);
        if (!response.IsSuccess)
            return BadRequest(response.Errors);

        return Ok();
    }
}
