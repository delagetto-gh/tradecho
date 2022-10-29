using System;
using System.Collections.Generic;

namespace Framework.Ddd;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot where TId : IEquatable<TId>
{
    private readonly List<Event> _events = new List<Event>();

    protected AggregateRoot(TId id) : base(id)
    { }

    protected AggregateRoot() { }

    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

    public void ClearEvents() => _events.Clear();
    
    protected void AddEvent(Event @event) => _events.Add(@event);
}