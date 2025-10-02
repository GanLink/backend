using System.Collections.Generic;

namespace GanLink.Shared.Domain.Model.Aggregates;

using GanLink.Shared.Domain.Model.Events;

public abstract class AggregateRoot
{
    private readonly List<DomainEvent> _domainEvents = new();
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
