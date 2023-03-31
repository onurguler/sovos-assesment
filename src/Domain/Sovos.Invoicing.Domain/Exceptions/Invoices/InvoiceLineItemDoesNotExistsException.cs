using Sovos.Invoicing.Domain.Core.Exceptions;
using Sovos.Invoicing.Domain.Primitives.Invoices;

namespace Sovos.Invoicing.Domain.Exceptions.Invoices;

public sealed class InvoiceLineItemDoesNotExistsException : SovosException
{
    public InvoiceLineItemDoesNotExistsException(InvoiceLineItemId lineItemId)
        : base($"Invoice line item does not exist with id '{lineItemId.Value}'")
    {
        LineItemId = lineItemId;
    }

    public InvoiceLineItemId LineItemId { get; }
}