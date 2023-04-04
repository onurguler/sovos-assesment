using Microsoft.Extensions.Configuration;

using Sovos.Invoicing.Application.Contracts.Emails;
using Sovos.Invoicing.Application.Core.Abstractions.Emails;
using Sovos.Invoicing.Application.Core.Abstractions.Notifications;
using Sovos.Invoicing.Domain.Core.Exceptions;

namespace Sovos.Invoicing.Infrastructure.Notifications;

public class EmailNotificationService : IEmailNotificationService
{
    private readonly IEmailService _emailService;
    private readonly IConfiguration _confguration;

    public EmailNotificationService(IEmailService emailService, IConfiguration confguration)
    {
        _emailService = emailService;
        _confguration = confguration;
    }
    public async Task SendInvoiceImportedEmail(InvoiceImportedEmail invoiceImportedEmail)
    {
        string? emailTo = _confguration.GetValue<string?>("Notifications:InvoiceImportedEmailTo", null);

        if (string.IsNullOrWhiteSpace(emailTo))
            throw new SovosException("App setting Notifications:InvoiceImportedEmailTo is empty");
        var mailRequest = new MailRequest(
            emailTo,
            "Fatura Aktarıldı",
            $"'{invoiceImportedEmail.InvoiceDate:d}' tarihli '{invoiceImportedEmail.InvoiceId}' numaralı fatura başarıyla sisteme aktarılmıştır.");

        await _emailService.SendEmailAsync(mailRequest);
    }

}