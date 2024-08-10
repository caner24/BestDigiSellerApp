using Asp.Versioning;
using BestDigiSellerApp.User.Api.ActionFilters;
using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Queries.Request;
using BestDigiSellerApp.User.Application.User.Queries.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.User.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]/")]
    [ApiVersion("1.0")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("addAdminUser")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddAdminUser([FromBody] CreateAdminCommandRequest createAdminCommandRequest)
        {
            var response = await _mediator.Send(createAdminCommandRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Errors);
            return Ok();
        }


        [HttpGet("getAllUserEmail")]
        public async Task<IActionResult> GetAllUserEmail()
        {
            var request = new GetAllUserQueryRequest();
            var response = await _mediator.Send(request);
            if (!response.IsSuccess)
                return BadRequest(response.Errors);

            return Ok(response.Value.Email);
        }
        [HttpDelete("deleteAdminUser/{Email}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteAdminUser([FromRoute] DeleteAdminCommandRequest deleteAdminCommandRequest)
        {
            var response = await _mediator.Send(deleteAdminCommandRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Errors);
            return Ok();
        }
    }
}
