using Jal.Monads;
using System.Collections.Generic;

namespace Beauty.Dick.Domain.Interface
{
    public interface IRepository<TEditable> where TEditable : class, IAggregateRoot
    {
        Result<bool> Exists(ISpecification<TEditable> spec);

        Result<TEditable> Fetch(ISpecification<TEditable> spec);

        Result<IEnumerable<TEditable>> FetchAll(ISpecification<TEditable> spec);

        Result<TEditable> Create(TEditable entity);

        Result<TEditable> Update(TEditable entity);

        Result<TEditable> Delete(ISpecification<TEditable> spec);
    }
}
