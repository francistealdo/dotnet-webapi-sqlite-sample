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

        public TEntity Add(TEntity entity)
        {
            try
            {
                appDbContext.Entry(entity).State = EntityState.Added;
                appDbContext.Set<TEntity>().Add(entity);
                appDbContext.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity Update(TEntity entity)
        {
            try
            {
                appDbContext.Entry(entity).State = EntityState.Modified;
                appDbContext.Set<TEntity>().Update(entity);
                appDbContext.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var entity = appDbContext.Set<TEntity>().Find(id);

                if (entity == null)
                    return;

                appDbContext.Set<TEntity>().Remove(entity);
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
