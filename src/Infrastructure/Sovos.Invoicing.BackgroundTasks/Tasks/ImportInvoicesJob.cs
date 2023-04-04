using System.Text.Json;

using Hangfire;

using Microsoft.Extensions.Logging;

using Sovos.Invoicing.Application.Contracts.Emails;
using Sovos.Invoicing.Application.Contracts.Invoices;
using Sovos.Invoicing.Application.Core.Abstractions.Notifications;
using Sovos.Invoicing.Application.Core.Data;
using Sovos.Invoicing.BackgroundTasks.Absractions.Tasks;
using Sovos.Invoicing.Domain.Entities.Invoices;
using Sovos.Invoicing.Domain.Primitives.Invoices;
using Sovos.Invoicing.Domain.Repositories.Invoices;
using Sovos.Invoicing.Domain.ValueObjects;

namespace Sovos.Invoicing.BackgroundTasks.Tasks;

public class ImportInvoicesJob : IBackgroundTask
{
    private readonly ILogger<ImportInvoicesJob> _logger;
    private readonly IDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInvoiceQueueRepository _invoiceQueueRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IEmailNotificationService _emailNotificationService;

    public ImportInvoicesJob(
        ILogger<ImportInvoicesJob> logger,
        IDbContext dbContext,
        IUnitOfWork unitOfWork,
        IInvoiceQueueRepository invoiceQueueRepository,
        IInvoiceRepository invoiceRepository,
        IEmailNotificationService emailNotificationService)
    {
        _logger = logger;
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        _invoiceQueueRepository = invoiceQueueRepository;
        _invoiceRepository = invoiceRepository;
        _emailNotificationService = emailNotificationService;
    }

    public async Task HandleAsync()
    {
        var pendingInvoices = await _invoiceQueueRepository.GetPendingAsync();

        _logger.LogInformation($"Starting {nameof(ImportInvoicesJob)} job with ${pendingInvoices.Count} pending invoices...");
        
        int successCount = 0;

        foreach (InvoiceQueue pendingInvoice in pendingInvoices)
        {
            try
            {
                var invoiceRequest = JsonSerializer.Deserialize<CreateInvoiceRequest>(pendingInvoice.InvoiceJson);

                if (invoiceRequest is null)
                {
                    pendingInvoice.Failed("Invoice json can not be deserialized.");
                    await _unitOfWork.SaveChangesAsync();
                    continue;
                }

                var invoice = Invoice.Create(
                    new InvoiceId(invoiceRequest.InvoiceHeader.InvoiceId),
                    new Name(invoiceRequest.InvoiceHeader.SenderTitle),
                    new Name(invoiceRequest.InvoiceHeader.ReceiverTitle),
                    invoiceRequest.InvoiceHeader.Date,
                    invoiceRequest.InvoiceLine.Select(s =>
                        InvoiceLineItem.Create(
                            new Name(s.Name),
                            s.Quantity,
                            new UnitCode(s.UnitCode),
                            s.UnitPrice
                        )
                    ).ToList()
                );

                _invoiceRepository.Insert(invoice);

                pendingInvoice.Execute();

                try
                {
                    await _unitOfWork.SaveChangesAsync();
                }
                catch(Exception)
                {
                    _dbContext.Set<Invoice>().Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                    throw;
                }

                successCount++;

                BackgroundJob.Enqueue(() 
                    => _emailNotificationService.SendInvoiceImportedEmail(
                        new InvoiceImportedEmail(invoice.InvoiceId.Value, invoice.Date)
                    )
                );
            }
            catch (Exception ex)
            {
                pendingInvoice.Failed(ex.Message);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        _logger.LogInformation($"End {nameof(ImportInvoicesJob)} job. Succeded: {successCount}, Failed: {pendingInvoices.Count - successCount}");
    }
}