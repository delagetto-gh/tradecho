using Microsoft.EntityFrameworkCore;

namespace Framework.Outbox;

public interface IOutboxServiceFactory
{
    IOutboxService Create<TDbContext>(TDbContext dbContext) where TDbContext : DbContext;
}
