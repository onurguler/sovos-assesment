using Sovos.Invoicing.Domain.Primitives.Invoices;
using Sovos.Invoicing.Domain.ValueObjects;

namespace Sovos.Invoicing.Domain.Entities.Invoices;

public sealed class InvoiceLineItem
{
    private InvoiceLineItem()
    {
    }

    private InvoiceLineItem(Name name, decimal quantity, UnitCode unitCode, decimal unitPrice)
    {
        Name = name;
        Quantity = quantity;
        UnitCode = unitCode;
        UnitPrice = unitPrice;
    }

    public InvoiceLineItemId Id { get; private set; } = null!;
    public InvoiceId InvoiceId { get; private set; } = null!;
    public Name Name { get; private set; } = null!;
    public decimal Quantity { get; private set; }
    public UnitCode UnitCode { get; private set; } = null!;
    public decimal UnitPrice { get; private set; }

    public static InvoiceLineItem Create(Name name, decimal quantity, UnitCode unitCode, decimal unitPrice)
    {
        return new InvoiceLineItem(name, quantity, unitCode, unitPrice);
    }
}