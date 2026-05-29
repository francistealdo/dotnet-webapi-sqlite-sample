namespace ProductAPI.Domain.Core.Interface.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(int id);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
    }
}
