using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Sovos.Invoicing.Domain.Entities.Invoices;
using Sovos.Invoicing.Domain.Primitives.Invoices;
using Sovos.Invoicing.Domain.ValueObjects;

namespace Sovos.Invoicing.Persistence.Configurations;

public sealed class InvoiceLineItemConfiguration : IEntityTypeConfiguration<InvoiceLineItem>
{
    public void Configure(EntityTypeBuilder<InvoiceLineItem> builder)
    {
        builder.ToTable("InvoiceLineItems");

        builder.HasKey(invoiceLineItem => invoiceLineItem.Id);

        builder.Property(invoiceLineItem => invoiceLineItem.Id)
            .HasConversion(invoiceLineItemId => invoiceLineItemId.Value, invoiceLineItemId => new InvoiceLineItemId(invoiceLineItemId))
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn()
            .IsRequired();

        builder.Property(invoiceLineItem => invoiceLineItem.InvoiceId)
            .HasConversion(invoiceId => invoiceId.Value, invoiceId => new InvoiceId(invoiceId))
            .IsRequired();

        builder.OwnsOne(invoiceLineItem => invoiceLineItem.Name, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(name => name.Value)
                .HasColumnName(nameof(InvoiceLineItem.Name))
                .HasMaxLength(Name.MaxLength)
                .IsRequired();
        });

        builder.Property(invoiceLineItem => invoiceLineItem.Quantity).IsRequired();

        builder.OwnsOne(invoiceLineItem => invoiceLineItem.UnitCode, unitCodeBuilder =>
        {
            unitCodeBuilder.WithOwner();

            unitCodeBuilder.Property(unitCode => unitCode.Value)
                .HasColumnName(nameof(InvoiceLineItem.UnitCode))
                .HasMaxLength(UnitCode.MaxLength)
                .IsRequired();
        });

        builder.Property(invoiceLineItem => invoiceLineItem.UnitPrice).IsRequired();

        builder.HasOne<Invoice>()
            .WithMany(invoice => invoice.LineItems)
            .HasForeignKey(invoiceLineItem => invoiceLineItem.InvoiceId)
            .HasPrincipalKey(invoice => invoice.InvoiceId)
            .IsRequired();
    }
}