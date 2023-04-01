using Sovos.Invoicing.Application.Contracts.Invoices;
using Sovos.Invoicing.Application.Core.Abstractions.Messaging;

namespace Sovos.Invoicing.Application.Invoices.Queries.GetDocumentById;

public class GetDocumentByIdQuery : IQuery<InvoiceResponse>
{
    public GetDocumentByIdQuery(string invoiceId)
    {
        InvoiceId = invoiceId;
    }

    public string InvoiceId { get; }
}
