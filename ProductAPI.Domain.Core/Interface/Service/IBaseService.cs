namespace ProductAPI.Domain.Core.Interface.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void Delete(TEntity obj);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
    }
}
