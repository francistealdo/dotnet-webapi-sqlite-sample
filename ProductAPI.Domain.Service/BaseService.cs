using ProductAPI.Domain.Core.Interface.Repository;
using ProductAPI.Domain.Core.Interface.Service;

namespace ProductAPI.Domain.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public TEntity Add(TEntity entity)
        {
            return repository.Add(entity);
        }

        public TEntity Update(TEntity entity)
        {
            return repository.Update(entity);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return repository.GetAll();
        }

        public TEntity GetById(int id)
        {
            return repository.GetById(id);
        }
    }
}
