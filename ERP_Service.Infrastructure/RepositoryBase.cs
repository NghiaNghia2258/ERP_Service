using ERP_Service.Domain.Abstractions;
using ERP_Service.Domain.Abstractions.Model;
using ERP_Service.Domain.Abstractions.RepositoryBase;
using ERP_Service.Domain.Const;
using ERP_Service.Shared.Exceptions;
using ERP_Service.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ERP_Service.Infrastructure;

	public abstract class RepositoryBase<T,TKey>: IRepositoryBase<T,TKey> where T : EntityBase<TKey>
{ 
   
    protected readonly AppDbContext _dbContext;
    protected RepositoryBase(AppDbContext dbContext)
    {
			_dbContext = dbContext;

		}
    public async Task UpdateAsync(T update, PayloadToken payloadToken)
    {
        if (_dbContext.Entry(update).State == EntityState.Unchanged) return;
        T? exist = _dbContext.Set<T>().Find(update.Id);
        if (exist == null) { throw new NotFoundDataException("Record for update not found"); }
        if (exist.Version == update.Version)
        {
            update.Version += 1;
        }
        else { throw new VersionIsOldException(); }
        if (update is IUpdateTracking trackingEntity)
        {
            trackingEntity.UpdatedAt = TimeConst.Now;
            trackingEntity.UpdatedBy = payloadToken.Username;
            trackingEntity.UpdatedName = payloadToken.FullName;
        }
        _dbContext.Entry(exist).CurrentValues.SetValues(update);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<TKey> CreateAsync(T entity, PayloadToken? payloadToken = null)
    {
        T? exist = _dbContext.Set<T>().Find(entity.Id);
        if (exist != null) { throw new RecordAlreadyExistsException(); }
        if (entity is ICreateTracking createTracking)
        {
            createTracking.CreatedAt = TimeConst.Now;
            createTracking.CreatedBy = payloadToken.UserLoginId.ToString();
            createTracking.CreatedName = payloadToken.FullName;
        }
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }
    public async Task DeleteAsync(TKey primaykey, PayloadToken payloadToken)
    {
        T? exist = _dbContext.Set<T>().Find(primaykey);
        if (exist == null) { throw new NotFoundDataException("Record for delete does not exist"); }
        if (exist is ISoftDelete softDelete)
        {
            softDelete.IsDeleted = true;
            softDelete.DeletedBy = payloadToken.Username;
            softDelete.DeletedAt = TimeConst.Now;
            softDelete.DeletedName = payloadToken.FullName;
        }
        else
        {
            _dbContext.Set<T>().Remove(exist);
        }
        await _dbContext.SaveChangesAsync();
    }
    public IQueryable<T> FindAll(bool trackChanges = false)
    {
        return !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();
    }

    public async Task<T?> GetByIdAsync(TKey primaryKey)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(primaryKey));
    }

    public async Task<T?> GetByIdAsync(TKey primaryKey, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = _dbContext.Set<T>().Where(x => x.Id.Equals(primaryKey));
        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        return await items.FirstOrDefaultAsync();
    }
}
