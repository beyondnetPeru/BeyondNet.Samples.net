namespace BeyondNet.App.Ums.Domain.Common.Interface
{
    public interface IRepository<TEntity, in TType> : IReadOnlyRepository<TEntity, TType> where TEntity : class, IAggregateRoot
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TType id);
    }
}
