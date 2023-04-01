namespace Sovos.Invoicing.Domain.Core.Exceptions;

public class ConflictException : SovosException
{
    public ConflictException(string message)
        : base(message)
    {
        Code = "Common.Conflict";
    }
}
