using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Ddd;

public interface IRepository<TAggregateRoot, TAggregateRootId> where TAggregateRoot : AggregateRoot<TAggregateRootId>
                                                               where TAggregateRootId : IEquatable<TAggregateRootId>
{
    Task AddAsync(TAggregateRoot entity);
    Task<TAggregateRoot> GetAsync(TAggregateRootId id);
    Task<IReadOnlyCollection<TAggregateRoot>> GetAsync(Expression<Func<TAggregateRoot, bool>> predicate);
    Task UpdateAsync(TAggregateRoot entity);
    Task RemoveAsync(TAggregateRoot entity);
}