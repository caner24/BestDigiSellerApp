
using Asp.Versioning;
using BestDigiSellerApp.Wallet.Application.Wallet.Commands.Request;
using BestDigiSellerApp.Wallet.Application.Wallet.Queries.Request;
using BestDigiSellerApp.Wallet.Entity.Dto;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.Wallet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    [ApiVersion("1.0")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public WalletController(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("createWallet")]
        public async Task<IActionResult> CreateWallet(CreateWalletCommandRequest createWalletCommandRequest)
        {
            var response = await _mediator.Send(createWalletCommandRequest);
            if (response.IsFailed)
                return BadRequest(response.Errors);

            await _publishEndpoint.Publish<WalletCreatedDto>(new WalletCreatedDto { UserEmail = response.Value.UserEmail, Currency = response.Value.Currency, Iban = response.Value.Iban });
            return StatusCode(201, response.Value);
        }


        [HttpPost("addFundsToWallet")]
        public async Task<IActionResult> AddFundsToWallet([FromBody]AddFundsToWalletCommandRequest addFundsToWalletCommandRequest)
        {
            var response = await _mediator.Send(addFundsToWalletCommandRequest);
            if (response.IsFailed)
                return BadRequest(response.Errors);

            return Ok();
        }

        [HttpGet("getWalletBalance")]
        public async Task<IActionResult> GetWalletBalance([FromQuery] GetWalletBalanceQueryRequest getWalletBalanceQueryRequest)
        {
            var response = await _mediator.Send(getWalletBalanceQueryRequest);
            if (response.IsFailed)
                return BadRequest(response.Errors);

            return Ok(response.Value);
        }
    }
}
