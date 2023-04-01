using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MediatR;

using Sovos.Invoicing.Application.Contracts.Invoices;
using Sovos.Invoicing.Application.Invoices.Commands.UploadDocument;
using Sovos.Invoicing.Application.Invoices.Queries.GetDocumentList;
using Sovos.Invoicing.Application.Invoices.Queries.GetDocumentById;

namespace Sovos.Invoicing.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class InvoicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> UploadDocument([FromBody] CreateInvoiceRequest request)
    {
        var command = new UploadDocumentCommand(JsonSerializer.Serialize(request));
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<InvoiceHeaderResponse>>> ListDocuments()
        => Ok(await _mediator.Send(new GetDocumentListQuery()));

    [HttpGet("{InvoiceId}")]
    public async Task<ActionResult<InvoiceResponse>> GetDocument([Bind("InvoiceId")] string invoiceId)
    {
        var query = new GetDocumentByIdQuery(invoiceId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}