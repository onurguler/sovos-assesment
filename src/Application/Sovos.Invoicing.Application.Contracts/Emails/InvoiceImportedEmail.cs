namespace Sovos.Invoicing.Application.Contracts.Emails;

public class InvoiceImportedEmail
{
    public InvoiceImportedEmail(string invoiceId, DateOnly invoiceDate)
    {
        InvoiceId = invoiceId;
        InvoiceDate = invoiceDate;
    }

    public string InvoiceId { get; }
    public DateOnly InvoiceDate { get; }
}