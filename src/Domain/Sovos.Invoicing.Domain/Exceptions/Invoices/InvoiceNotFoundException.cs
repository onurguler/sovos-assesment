using Sovos.Invoicing.Domain.Core.Exceptions;
using Sovos.Invoicing.Domain.Primitives.Invoices;

namespace Sovos.Invoicing.Domain.Exceptions.Invoices;

public class InvoiceNotFoundException : NotFoundException
{
    public InvoiceNotFoundException(InvoiceId message) 
        : base($"Invoice does not exist with id '{message.Value}'")
    {
        Code = "Invoices.InvoiceNotFound";
    }
}