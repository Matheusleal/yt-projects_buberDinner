namespace BuberDinner.Domain.Common.Models;

// The only reason of this interface is to map DomainEvents on the publisher interceptor
public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }

    public void ClearDomainEvents();
}
