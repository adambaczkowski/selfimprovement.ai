using System.Linq.Expressions;
using LS.Common;

namespace IdentityApi.Data;

public class UserRepository : IGenericRepository<Models.User>
{
    private readonly IdentityDbContext _dbContext;
    public UserRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public IEnumerable<Models.User> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<Models.User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Models.User GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Models.User GetByIdWithIncludes(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Models.User> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Models.User> GetByIdWithIncludesAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public bool Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Add(in Models.User sender)
    {
        throw new NotImplementedException();
    }

    public void Update(in Models.User sender)
    {
        throw new NotImplementedException();
    }

    public int Save()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveAsync()
    {
        throw new NotImplementedException();
    }

    public Models.User Select(Expression<Func<Models.User, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<Models.User> SelectAsync(Expression<Func<Models.User, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}