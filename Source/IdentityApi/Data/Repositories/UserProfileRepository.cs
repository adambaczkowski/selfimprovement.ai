using System.Linq.Expressions;
using IdentityApi.Models;
using LS.Common;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Data;

public class UserProfileRepository : IGenericRepository<UserProfile>
{
    private readonly IdentityDbContext _dbContext;

    public UserProfileRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<UserProfile> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<UserProfile>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public UserProfile GetById(Guid id)
    {
        return _dbContext.UserProfiles.Find(id);
    }

    public UserProfile GetByIdWithIncludes(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserProfile> GetByIdAsync(Guid id)
    {
        return await _dbContext.UserProfiles.FindAsync(id);
    }

    public Task<UserProfile> GetByIdWithIncludesAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public bool Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Add(in UserProfile sender)
    {
        _dbContext.Add(sender).State = EntityState.Added;
    }

    public void Update(in UserProfile sender)
    {
        _dbContext.Entry(sender).State = EntityState.Modified;
    }

    public int Save()
    {
        return _dbContext.SaveChanges();
    }

    public Task<int> SaveAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

    public UserProfile Select(Expression<Func<UserProfile, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<UserProfile> SelectAsync(Expression<Func<UserProfile, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}