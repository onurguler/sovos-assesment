using Sovos.Invoicing.Application.Contracts.Invoices;
using Sovos.Invoicing.Application.Core.Abstractions.Messaging;
using Sovos.Invoicing.Domain.Repositories.Invoices;

namespace Sovos.Invoicing.Application.Invoices.Queries.GetDocumentList;

public class GetDocumentListQueryHandler : IQueryHandler<GetDocumentListQuery, List<InvoiceHeaderResponse>>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public GetDocumentListQueryHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<List<InvoiceHeaderResponse>> Handle(GetDocumentListQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _invoiceRepository.GetAllAsync(cancellationToken);

        var response = invoices.Select(
            s => new InvoiceHeaderResponse(
                s.InvoiceId.Value,
                s.SenderTitle.Value,
                s.ReceiverTitle.Value,
                s.Date
            )
        ).ToList();

        return response;
    }
}
