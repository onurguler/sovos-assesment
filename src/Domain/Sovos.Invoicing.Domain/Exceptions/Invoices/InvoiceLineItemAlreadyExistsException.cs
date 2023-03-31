using Sovos.Invoicing.Domain.Core.Exceptions;
using Sovos.Invoicing.Domain.Primitives.Invoices;

namespace Sovos.Invoicing.Domain.Exceptions.Invoices;

public sealed class InvoiceLineItemAlreadyExistsException : SovosException
{
    public InvoiceLineItemAlreadyExistsException(InvoiceLineItemId lineItemId)
        : base($"Invoice line item already exists with id '{lineItemId.Value}'")
    {
        LineItemId = lineItemId;
    }

    public InvoiceLineItemId LineItemId { get; }
}
