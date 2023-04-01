using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Sovos.Invoicing.Domain.Entities.Invoices;
using Sovos.Invoicing.Domain.Primitives.Invoices;
using Sovos.Invoicing.Domain.ValueObjects;

namespace Sovos.Invoicing.Persistence.Configurations;

public sealed class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");

        builder.HasKey(invoice => invoice.InvoiceId);

        builder.Property(invoice => invoice.InvoiceId)
            .HasConversion(invoiceId => invoiceId.Value, invoiceId => new InvoiceId(invoiceId))
            .HasMaxLength(InvoiceId.MaxLength)
            .IsRequired();

        builder.OwnsOne(invoice => invoice.SenderTitle, senderTitleBuilder =>
        {
            senderTitleBuilder.WithOwner();

            senderTitleBuilder.Property(senderTitle => senderTitle.Value)
                .HasColumnName(nameof(Invoice.SenderTitle))
                .HasMaxLength(Name.MaxLength)
                .IsRequired();
        });

        builder.OwnsOne(invoice => invoice.ReceiverTitle, receiverTitleBuilder =>
        {
            receiverTitleBuilder.WithOwner();

            receiverTitleBuilder.Property(receiverTitle => receiverTitle.Value)
                .HasColumnName(nameof(Invoice.ReceiverTitle))
                .HasMaxLength(Name.MaxLength)
                .IsRequired();
        });

        builder.Property(invoice => invoice.Date).IsRequired();
    }
}
