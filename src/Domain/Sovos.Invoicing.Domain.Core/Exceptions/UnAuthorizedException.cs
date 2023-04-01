namespace Sovos.Invoicing.Domain.Core.Exceptions;

public class UnAuthorizedException : SovosException
{
    public UnAuthorizedException(string message)
        : base(message)
    {
        Code = "Common.Unauthorized";
    }
}
