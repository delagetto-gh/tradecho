using System.Collections.Generic;

namespace Framework.Ddd
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<Event> Events { get; }
        
        void ClearEvents();
    }
}