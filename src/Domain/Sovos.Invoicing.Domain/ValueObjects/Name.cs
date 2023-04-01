using Sovos.Invoicing.Domain.Core.Primitives;

namespace Sovos.Invoicing.Domain.ValueObjects;

public sealed class Name : ValueObject
{
    public const int MaxLength = 100;

    private Name() {}
    
    public Name(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Name cannot be longer than {MaxLength} characters.", nameof(name));
        }

        Value = name;
    }

    public string Value { get; } = null!;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}