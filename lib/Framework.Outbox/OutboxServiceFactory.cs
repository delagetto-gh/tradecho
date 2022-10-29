using Microsoft.EntityFrameworkCore;

namespace Framework.Outbox;

internal class OutboxServiceFactory : IOutboxServiceFactory
{
    public IOutboxService Create<TDbContext>(TDbContext dbContext) where TDbContext : DbContext
    {
        return new OutboxService(dbContext);
    }
}
