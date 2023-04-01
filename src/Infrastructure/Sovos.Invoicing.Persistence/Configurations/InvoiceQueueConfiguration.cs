using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Sovos.Invoicing.Domain.Entities.Invoices;
using Sovos.Invoicing.Domain.Primitives.Invoices;

namespace Sovos.Invoicing.Persistence.Configurations;

public class InvoiceQueueConfiguration : IEntityTypeConfiguration<InvoiceQueue>
{
    public void Configure(EntityTypeBuilder<InvoiceQueue> builder)
    {
        builder.ToTable("InvoiceQueue");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new InvoiceQueueId(x))
            .ValueGeneratedNever();

        builder.Property(x => x.InvoiceJson).IsRequired();

        builder.Property(x => x.CreatedAtUtc).IsRequired();

        builder.Property(x => x.ExecutedAtUtc);

        builder.Property(x => x.LastExecutedAtUtc);

        builder.Property(x => x.RetryCount).IsRequired();

        builder.Property(x => x.FailureReason);

        builder.Property(x => x.Completed).IsRequired();
    }
}