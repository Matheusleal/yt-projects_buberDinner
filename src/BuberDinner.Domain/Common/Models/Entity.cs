namespace BuberDinner.Domain.Common.Models;
public class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public TId Id { get; protected set; }

    protected Entity(TId id) => Id = id;

    public override bool Equals(object? obj) => obj is Entity<TId> entity && Id.Equals(entity.Id);
    public override int GetHashCode() => HashCode.Combine(Id);
    public static bool operator ==(Entity<TId> left, Entity<TId> right) => Equals(left, right);
    public static bool operator !=(Entity<TId> left, Entity<TId> right) => !Equals(left, right);

    public bool Equals(Entity<TId>? other) => Equals(other as object);

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    // only for efCore mapping
#pragma warning disable CS8618
    public Entity()
    {
    }
#pragma warning restore CS8618
}
