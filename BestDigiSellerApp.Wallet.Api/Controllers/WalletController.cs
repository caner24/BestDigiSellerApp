
using BestDigiSellerApp.Wallet.Application.Wallet.Commands.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.Wallet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createWallet")]
        public async Task<IActionResult> CreateWallet(CreateWalletCommandRequest createWalletCommandRequest)
        {
            var response = await _mediator.Send(createWalletCommandRequest);
            return StatusCode(201, response);
        }
    }
}
