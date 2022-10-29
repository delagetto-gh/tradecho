using MediatR;

namespace Contracts.Commands;

public record EchoWord(string Text) : IRequest { }