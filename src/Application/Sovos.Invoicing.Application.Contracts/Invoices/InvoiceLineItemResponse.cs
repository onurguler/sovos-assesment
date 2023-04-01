namespace Sovos.Invoicing.Application.Contracts.Invoices;

public record InvoiceLineItemResponse(int Id, string Name, decimal Quantity, string UnitCode, decimal UnitPrice);
