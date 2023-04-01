namespace Sovos.Invoicing.Domain.Core.Exceptions;

public class ForbiddenException : SovosException
{
    public ForbiddenException(string message)
        : base(message)
    {
        Code = "Common.Forbidden";
    }
}
