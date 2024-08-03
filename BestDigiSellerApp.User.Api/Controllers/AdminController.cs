using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.User.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]/")]
    public class AdminController : ControllerBase
    {
        [HttpPost("addAdminUser")]
        public IActionResult AddAdminUser()
        {
            return Ok();
        }
        [HttpPut("editAdminUser")]
        public IActionResult EditAdminUser()
        {
            return Ok();
        }
        [HttpDelete("deleteAdminUser")]
        public IActionResult DeleteAdminUser()
        {
            return Ok();
        }
    }
}
