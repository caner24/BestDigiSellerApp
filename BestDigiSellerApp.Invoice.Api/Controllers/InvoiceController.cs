using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.Invoice.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize("Admin")]
public class InvoiceController : ControllerBase
{
    private readonly IMediator _mediator;
    public InvoiceController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("getInvoice")]
    public IActionResult GetInvoice()
    {
        return Ok();
    }

    [HttpPost("createInvoice")]
    public IActionResult CreateInvoice()
    {
        return Ok();
    }
}
