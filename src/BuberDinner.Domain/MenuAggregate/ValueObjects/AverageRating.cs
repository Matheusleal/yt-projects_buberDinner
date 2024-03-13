using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.MenuAggregate.ValueObjects;

public class AverageRating : ValueObject
{
    public int NumRatings { get; private set; }
    public float Value { get; private set; }

    private AverageRating(int numRatings, float value)
    {
        NumRatings = numRatings;
        Value = value;
    }

    public static AverageRating Create(
        int numRatings,
        float value)
    {
        return new (numRatings, value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
