using Sovos.Invoicing.Application.Contracts.Invoices;
using Sovos.Invoicing.Application.Core.Abstractions.Messaging;
using Sovos.Invoicing.Domain.Exceptions.Invoices;
using Sovos.Invoicing.Domain.Primitives.Invoices;
using Sovos.Invoicing.Domain.Repositories.Invoices;

namespace Sovos.Invoicing.Application.Invoices.Queries.GetDocumentById;

public class GetDocumentByIdQueryHandler : IQueryHandler<GetDocumentByIdQuery, InvoiceResponse>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public GetDocumentByIdQueryHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<InvoiceResponse> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var invoiceId = new InvoiceId(request.InvoiceId);
        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId, cancellationToken);

        if (invoice == null)
            throw new InvoiceNotFoundException(invoiceId);

        var response = new InvoiceResponse(
            new InvoiceHeaderResponse(
                invoice.InvoiceId.Value,
                invoice.SenderTitle.Value,
                invoice.ReceiverTitle.Value,
                invoice.Date
            ),
            invoice.LineItems.Select(
                s => new InvoiceLineItemResponse(
                    s.Id.Value,
                    s.Name.Value,
                    s.Quantity,
                    s.UnitCode.Value,
                    s.UnitPrice
                )
            ).ToList()
        );

        return response;
    }
}
