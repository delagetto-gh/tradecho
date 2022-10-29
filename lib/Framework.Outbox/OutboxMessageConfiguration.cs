
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Outbox;

internal class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    private const string TableName = "Outbox";

    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder
        .ToTable(TableName)
        .HasKey(o => o.Id);

        builder
        .Property(o => o.ProcessedAt);
    }
}
