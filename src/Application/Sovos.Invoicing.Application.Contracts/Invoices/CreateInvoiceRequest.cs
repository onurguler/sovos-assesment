namespace Sovos.Invoicing.Application.Contracts.Invoices;

public record CreateInvoiceRequest(CreateInvoiceHeaderRequest InvoiceHeader, List<CreateInvoiceLineItemRequest> InvoiceLine);
