namespace Sovos.Invoicing.Application.Contracts.Invoices;

public record InvoiceResponse(InvoiceHeaderResponse InvoiceHeader, List<InvoiceLineItemResponse> InvoiceLine);