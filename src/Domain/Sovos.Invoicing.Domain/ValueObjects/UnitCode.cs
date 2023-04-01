using Sovos.Invoicing.Domain.Core.Primitives;

namespace Sovos.Invoicing.Domain.ValueObjects;

public sealed class UnitCode : ValueObject
{
    public const int MaxLength = 50;

    private UnitCode() { }

    public UnitCode(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"Unit code cannot be longer than {MaxLength} characters.", nameof(value));
        }

        Value = value;
    }

    public string Value { get; } = null!;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}