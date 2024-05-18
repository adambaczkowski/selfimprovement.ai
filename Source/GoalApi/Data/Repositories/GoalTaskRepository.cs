using System.Linq.Expressions;
using LS.Common;
using Microsoft.EntityFrameworkCore;

namespace GoalApi.Data.Repositories;

public class GoalTaskRepository(GoalDbContext dbContext) : IGenericRepository<Models.GoalTask>
{

    public IQueryable<Models.GoalTask> GetQuery()
    {
        return dbContext.GoalTasks.AsQueryable();
    }

    public IEnumerable<Models.GoalTask> GetAll()
    {
        return dbContext.GoalTasks.ToList();
    }

    public async Task<List<Models.GoalTask>> GetAllAsync()
    {
        return await dbContext.GoalTasks.ToListAsync();
    }

    public Models.GoalTask GetById(Guid id)
    {
        return dbContext.GoalTasks.Find(id);
    }

    public Models.GoalTask GetByIdWithIncludes(Guid id)
    {
        return dbContext.GoalTasks.Include(x => x.Goal)
            .FirstOrDefault(x => x.Id == id);
    }

    public async Task<Models.GoalTask> GetByIdAsync(Guid id)
    {
        return await dbContext.GoalTasks.FindAsync(id);
    }

    public async Task<Models.GoalTask> GetByIdWithIncludesAsync(Guid id)
    {
        return dbContext.GoalTasks.Include(x => x.Goal)
            .FirstOrDefault(x => x.Id == id);
    }

    public bool Remove(Guid id)
    {
        var goalTask = dbContext.GoalTasks.Single(x => x.Id == id);
        var entityEntry = dbContext.Remove(goalTask);

        return entityEntry.State == EntityState.Deleted;
    }

    public void Add(in Models.GoalTask sender)
    {
        dbContext.GoalTasks.Add(sender).State = EntityState.Added;
    }

    public void Update(in Models.GoalTask sender)
    {
        dbContext.GoalTasks.Update(sender).State = EntityState.Modified;
    }

    public int Save()
    {
        return dbContext.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public Models.GoalTask Select(Expression<Func<Models.GoalTask, bool>> predicate)
    {
        return dbContext.GoalTasks.Where(predicate).FirstOrDefault();
    }

    public async Task<Models.GoalTask> SelectAsync(Expression<Func<Models.GoalTask, bool>> predicate)
    {
        return await dbContext.GoalTasks.Where(predicate).FirstOrDefaultAsync();
    }
}