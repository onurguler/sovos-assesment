namespace Sovos.Invoicing.Domain.Core.Exceptions;

public class BadRequestException : SovosException
{
    public BadRequestException(string message)
        : base(message)
    {
        Code = "Common.BadRequest";
    }
}
