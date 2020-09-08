namespace Template.BL.Management.GenericRepository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Template.DAL;
    using Template.DAL.GenericEntity;

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DataContext _dataContext;

        public GenericRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return this._dataContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public Task<TEntity> GetByIdAsync(Guid id)
        {
            return this._dataContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await this._dataContext.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
            await this._dataContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task UpdateAsync(TEntity entity)
        {
            this._dataContext.Set<TEntity>().Update(entity);
            return this._dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await this.GetByIdAsync(id).ConfigureAwait(false);

            this._dataContext.Set<TEntity>().Remove(entity);
            await this._dataContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}