namespace Sovos.Invoicing.Application.Contracts.Invoices;

public record InvoiceHeaderResponse(string InvoiceId, string SenderTitle, string ReceiverTitle, DateOnly Date);
