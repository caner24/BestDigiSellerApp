using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.Invoice.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize("Admin")]
public class InvoiceController : ControllerBase
{
    public InvoiceController()
    {
        
    }
    [HttpGet("getInvoice")]
    public IActionResult GketInvoice()
    {
        return Ok();
    }
}
