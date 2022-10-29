using MediatR;

namespace Contracts.Events;

public record WordEchoed(string Word) : INotification { } 