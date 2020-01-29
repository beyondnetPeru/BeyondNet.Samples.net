using System.Collections.Generic;

namespace BeyondNet.App.Ums.Domain.Common.Interface
{
    public interface IReadOnlyRepository<TEntity, in TType> where TEntity : class, IAggregateRoot
    {
        TEntity Get(TType id);

        TEntity FindOne(ISpecification<TEntity> spec);

        IEnumerable<TEntity> Find(ISpecification<TEntity> spec);
    }
}
