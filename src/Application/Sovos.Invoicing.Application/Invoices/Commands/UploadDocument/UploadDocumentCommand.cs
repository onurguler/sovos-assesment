using MediatR;

using Sovos.Invoicing.Application.Core.Abstractions.Messaging;

namespace Sovos.Invoicing.Application.Invoices.Commands.UploadDocument;

public class UploadDocumentCommand : ICommand<Unit>
{
    public UploadDocumentCommand(string invoiceJson)
    {
        InvoiceJson = invoiceJson;
    }

    public string InvoiceJson { get; }
}
