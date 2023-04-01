namespace Sovos.Invoicing.Domain.Core.Exceptions;

public class InternalServerErrorException : SovosException
{
    public InternalServerErrorException(string message)
        : base(message)
    {
        Code = "Common.InternalServerError";
    }
}
