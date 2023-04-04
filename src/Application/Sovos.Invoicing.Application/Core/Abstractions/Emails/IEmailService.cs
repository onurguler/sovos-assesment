using Sovos.Invoicing.Application.Contracts.Emails;

namespace Sovos.Invoicing.Application.Core.Abstractions.Emails;

public interface IEmailService
{
    Task SendEmailAsync(MailRequest request);
}