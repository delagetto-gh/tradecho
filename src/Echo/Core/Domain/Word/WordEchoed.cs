
using Framework.Ddd;

namespace Domain.Word;

public record WordEchoed(string Word) : Event { }