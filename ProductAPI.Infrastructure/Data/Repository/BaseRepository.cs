using Microsoft.EntityFrameworkCore;
using ProductAPI.Domain.Core.Interface.Repository;

namespace ProductAPI.Infrastructure.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext appDbContext;

        public BaseRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public void Add(TEntity obj)
        {
            try
            {
                appDbContext.Entry(obj).State = EntityState.Added;
                appDbContext.Set<TEntity>().Add(obj);
                appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(TEntity obj)
        {
            try
            {
                appDbContext.Set<TEntity>().Remove(obj);
                appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return appDbContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return appDbContext.Set<TEntity>().Find(id);
        }
    }
}
