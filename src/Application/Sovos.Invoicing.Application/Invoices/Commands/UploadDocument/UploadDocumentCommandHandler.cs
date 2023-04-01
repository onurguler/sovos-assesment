using MediatR;

using Sovos.Invoicing.Application.Core.Abstractions.Messaging;
using Sovos.Invoicing.Application.Core.Data;
using Sovos.Invoicing.Domain.Entities.Invoices;
using Sovos.Invoicing.Domain.Primitives.Invoices;
using Sovos.Invoicing.Domain.Repositories.Invoices;

namespace Sovos.Invoicing.Application.Invoices.Commands.UploadDocument;

public class UploadDocumentCommandHandler : ICommandHandler<UploadDocumentCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInvoiceQueueRepository _invoiceQueueRepository;

    public UploadDocumentCommandHandler(IUnitOfWork unitOfWork, IInvoiceQueueRepository invoiceQueueRepository)
    {
        _unitOfWork = unitOfWork;
        _invoiceQueueRepository = invoiceQueueRepository;
    }

    public async Task<Unit> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        var invoiceQueue = InvoiceQueue.Create(
            new InvoiceQueueId(Guid.NewGuid()),
            request.InvoiceJson
        );

        _invoiceQueueRepository.Insert(invoiceQueue);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
