using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Framework.Outbox;

internal class OutboxModelCustomiser : RelationalModelCustomizer
{
    public OutboxModelCustomiser(ModelCustomizerDependencies dependencies) : base(dependencies)
    { }

    public override void Customize(ModelBuilder builder, DbContext context)
    {
        builder.ApplyConfiguration(new OutboxMessageConfiguration());
        base.Customize(builder, context);
    }
}
