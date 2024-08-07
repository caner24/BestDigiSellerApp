
using BestDigiSellerApp.Wallet.Application.Wallet.Commands.Request;
using BestDigiSellerApp.Wallet.Entity.Dto;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.Wallet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
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
    }
}
