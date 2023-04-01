namespace Sovos.Invoicing.Application.Contracts.Invoices;

public record CreateInvoiceHeaderRequest(string InvoiceId, string SenderTitle, string ReceiverTitle, DateOnly Date);
