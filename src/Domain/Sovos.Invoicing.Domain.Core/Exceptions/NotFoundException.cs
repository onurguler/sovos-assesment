namespace Sovos.Invoicing.Domain.Core.Exceptions;

public class NotFoundException : SovosException
{
    public NotFoundException(string message)
        : base(message)
    {
        Code = "Common.NotFound";
    }
}
