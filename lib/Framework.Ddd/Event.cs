using System;

namespace Framework.Ddd;

public abstract record Event
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
