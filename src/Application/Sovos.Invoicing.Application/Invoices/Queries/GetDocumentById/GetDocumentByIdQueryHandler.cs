using Microsoft.EntityFrameworkCore;

using Sovos.Invoicing.Application.Contracts.Invoices;
using Sovos.Invoicing.Application.Core.Abstractions.Messaging;
using Sovos.Invoicing.Application.Core.Data;
using Sovos.Invoicing.Domain.Entities.Invoices;
using Sovos.Invoicing.Domain.Exceptions.Invoices;
using Sovos.Invoicing.Domain.Primitives.Invoices;
using Sovos.Invoicing.Domain.Repositories.Invoices;

namespace Sovos.Invoicing.Application.Invoices.Queries.GetDocumentById;

public class GetDocumentByIdQueryHandler : IQueryHandler<GetDocumentByIdQuery, InvoiceResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IInvoiceRepository _invoiceRepository;

    public GetDocumentByIdQueryHandler(IDbContext dbContext, IInvoiceRepository invoiceRepository)
    {
        _dbContext = dbContext;
        _invoiceRepository = invoiceRepository;
    }

    public async Task<InvoiceResponse> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var invoiceId = new InvoiceId(request.InvoiceId);
        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId, cancellationToken);

        if (invoice == null)
            throw new InvoiceNotFoundException(invoiceId);

        var liteItems = await _dbContext.Set<InvoiceLineItem>()
            .Where(w => w.InvoiceId == invoiceId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var response = new InvoiceResponse(
            new InvoiceHeaderResponse(
                invoice.InvoiceId.Value,
                invoice.SenderTitle.Value,
                invoice.ReceiverTitle.Value,
                invoice.Date
            ),
            liteItems.Select(
                s => new InvoiceLineItemResponse(
                    s.Id.Value,
                    s.Name.Value,
                    s.Quantity,
                    s.UnitCode.Value,
                    s.UnitPrice
                )
            ).ToList()
        );

        return response;
    }
}
