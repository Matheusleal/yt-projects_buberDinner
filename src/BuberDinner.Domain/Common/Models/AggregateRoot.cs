namespace BuberDinner.Domain.Common.Models;
public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
    where TId : AggregateRootId<TIdType>
{
    protected AggregateRoot(TId id)
    {
        Id = id;
    }

    // only for efCore mapping
#pragma warning disable CS8618
    public AggregateRoot()
    {
    }
#pragma warning restore CS8618
}
