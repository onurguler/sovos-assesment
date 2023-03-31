using Sovos.Invoicing.Domain.Primitives.Invoices;

namespace Sovos.Invoicing.Domain.Entities.Invoices;

public sealed class InvoiceQueue
{
    private InvoiceQueue()
    {
    }

    private InvoiceQueue(InvoiceQueueId id, string invoiceJson)
    {
        Id = id;
        InvoiceJson = invoiceJson;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public InvoiceQueueId Id { get; private set; } = null!;
    public string InvoiceJson { get; private set; } = null!;
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? ExecutedAtUtc { get; private set; }
    public DateTime? LastExecutedAtUtc { get; private set; }
    public int RetryCount { get; private set; }
    public string? FailureReason { get; private set; }
    public bool Completed { get; private set; }

    public void Execute()
    {
        if (!ExecutedAtUtc.HasValue)
        {
            ExecutedAtUtc = DateTime.UtcNow;
            LastExecutedAtUtc = ExecutedAtUtc;
        }
        else
        {
            LastExecutedAtUtc = DateTime.UtcNow;
            RetryCount++;
        }

        FailureReason = null;
        Completed = true;
    }

    public void Failed(string reason)
    {
        Execute();

        FailureReason = reason;
        Completed = false;
    }

    public static InvoiceQueue Create(InvoiceQueueId id, string invoiceJson)
    {
        return new InvoiceQueue(id, invoiceJson);
    }
}