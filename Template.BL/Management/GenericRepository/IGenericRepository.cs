namespace Template.BL.Management.GenericRepository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(Guid id);

        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(Guid id);
    }
}