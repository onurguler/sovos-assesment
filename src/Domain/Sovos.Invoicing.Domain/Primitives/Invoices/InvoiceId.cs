namespace Sovos.Invoicing.Domain.Primitives.Invoices;


public sealed class InvoiceId
{
    public const int MaxLength = 50;

    public InvoiceId(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

        if (value.Length > MaxLength)
            throw new ArgumentException($"Invoice id cannot be longer than {MaxLength} characters.", nameof(value));

        Value = value;
    }

    public string Value { get; } = null!;
}
