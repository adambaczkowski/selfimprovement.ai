using System.Linq.Expressions;
using LS.Common;
using Microsoft.EntityFrameworkCore;

namespace GoalApi.Data.Repositories;

public class GoalTaskRepository : IGenericRepository<Models.GoalTask>
{
    private readonly GoalDbContext _dbContext;

    public GoalTaskRepository(GoalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Models.GoalTask> GetQuery()
    {
        return _dbContext.GoalTasks.AsQueryable();
    }

    public IEnumerable<Models.Goal> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<Models.GoalTask>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Models.GoalTask GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Models.GoalTask GetByIdWithIncludes(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Models.GoalTask> GetByIdAsync(Guid id)
    {
        return _dbContext.GoalTasks.SingleAsync(x => x.Id == id);
    }

    Task<Models.GoalTask> IGenericRepository<Models.GoalTask>.GetByIdWithIncludesAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public bool Remove(Guid id)
    {
        var goalTask = _dbContext.GoalTasks.Single(x => x.Id == id);
        var entityEntry = _dbContext.Remove(goalTask);

        return entityEntry.State == EntityState.Deleted;
    }

    IEnumerable<Models.GoalTask> IGenericRepository<Models.GoalTask>.GetAll()
    {
        throw new NotImplementedException();
    }

    public void Add(in Models.GoalTask sender)
    {
        throw new NotImplementedException();
    }

    public void Update(in Models.GoalTask sender)
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

    public Models.GoalTask Select(Expression<Func<Models.GoalTask, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<Models.GoalTask> SelectAsync(Expression<Func<Models.GoalTask, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}