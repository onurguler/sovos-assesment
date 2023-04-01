namespace Sovos.Invoicing.Application.Contracts.Invoices;

public record CreateInvoiceLineItemRequest(int Id, string Name, decimal Quantity, string UnitCode, decimal UnitPrice);
