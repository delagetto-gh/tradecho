using System;

namespace Framework.Outbox;

public class OutboxMessage
{
    public Guid Id { get; init; }

    public string Topic { get; init; }

    public string Content { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? ProcessedAt { get; internal set; }
}
