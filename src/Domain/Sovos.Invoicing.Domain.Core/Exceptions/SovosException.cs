namespace Sovos.Invoicing.Domain.Core.Exceptions;

public class SovosException : Exception
{
    public SovosException(string message)
        : base(message)
    {
    }

    public string? Code { get; set; }
}
