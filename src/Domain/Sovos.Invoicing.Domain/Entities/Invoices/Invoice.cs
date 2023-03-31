using Sovos.Invoicing.Domain.Exceptions.Invoices;
using Sovos.Invoicing.Domain.Primitives.Invoices;
using Sovos.Invoicing.Domain.ValueObjects;

namespace Sovos.Invoicing.Domain.Entities.Invoices;


public sealed class Invoice
{
    private readonly List<InvoiceLineItem> _lineItems = new();

    private Invoice()
    {
    }

    private Invoice(InvoiceId invoiceId, Name senderTitle, Name receiverTitle, DateOnly date, List<InvoiceLineItem>? lineItems)
    {
        InvoiceId = invoiceId;
        SenderTitle = senderTitle;
        ReceiverTitle = receiverTitle;
        Date = date;

        lineItems?.ForEach(AddLineItem);
    }

    public InvoiceId InvoiceId { get; private set; } = null!;
    public Name SenderTitle { get; private set; } = null!;
    public Name ReceiverTitle { get; private set; } = null!;
    public DateOnly Date { get; private set; }

    public IReadOnlyCollection<InvoiceLineItem> LineItems => _lineItems;

    public void ChangeSenderTitle(Name senderTitle)
    {
        SenderTitle = senderTitle;
    }

    public void ChangeReceiverTitle(Name receiverTitle)
    {
        ReceiverTitle = receiverTitle;
    }

    public void ChangeDate(DateOnly date)
    {
        Date = date;
    }

    public void AddLineItem(InvoiceLineItem lineItem)
    {
        if (_lineItems.Any(x => x.Id == lineItem.Id))
            throw new InvoiceLineItemAlreadyExistsException(lineItem.Id);

        _lineItems.Add(lineItem);
    }

    public void RemoveLineItem(InvoiceLineItemId lineItemId)
    {
        var lineItem = _lineItems.SingleOrDefault(x => x.Id == lineItemId);
        if (lineItem == null)
            throw new InvoiceLineItemDoesNotExistsException(lineItemId);

        _lineItems.Remove(lineItem);
    }

    public static Invoice Create(InvoiceId invoiceId, Name senderTitle, Name receiverTitle, DateOnly date, List<InvoiceLineItem>? lineItems = null)
    {
        return new Invoice(invoiceId, senderTitle, receiverTitle, date, lineItems);
    }
}