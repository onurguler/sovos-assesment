using Sovos.Invoicing.Application.Contracts.Emails;

namespace Sovos.Invoicing.Application.Core.Abstractions.Notifications;

public interface IEmailNotificationService
{
    Task SendInvoiceImportedEmail(InvoiceImportedEmail invoiceImportedEmail);
}