using System.Linq.Expressions;
using LS.Common;
using Microsoft.EntityFrameworkCore;

namespace GoalApi.Data.Repositories;

public class GoalRepository(GoalDbContext dbContext) : IGenericRepository<Models.Goal>
{
    public IQueryable<Models.Goal> GetQuery()
    {
        return dbContext.Goals.AsQueryable();
    }

    public IEnumerable<Models.Goal> GetAll()
    {
        return dbContext.Goals.ToList();
    }

    public Task<List<Models.Goal>> GetAllAsync()
    {
        return dbContext.Goals.ToListAsync();
    }

    public Models.Goal GetById(Guid id)
    {
        return dbContext.Goals.Find(id);
    }

    public Models.Goal GetByIdWithIncludes(Guid id)
    {
        return dbContext.Goals.Include(x => x.Tasks)
            .FirstOrDefault(x => x.Id == id);
    }

    public async Task<Models.Goal> GetByIdAsync(Guid id)
    {
        return await dbContext.Goals.FindAsync(id);
    }

    public Task<Models.Goal> GetByIdWithIncludesAsync(Guid id)
    {
        return dbContext.Goals.Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public bool Remove(Guid id)
    {
        var goal = dbContext.Goals.Find(id);
        if (goal is { })
        {
            dbContext.Goals.Remove(goal);
            return true;
        }

        return false;
    }

    public void Add(in Models.Goal sender)
    {
        dbContext.Goals.Add(sender).State = EntityState.Added;
    }

    public void Update(in Models.Goal sender)
    {
        dbContext.Update(sender).State = EntityState.Modified;
    }

    public int Save()
    {
        return dbContext.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public Models.Goal Select(Expression<Func<Models.Goal, bool>> predicate)
    {
        return dbContext.Goals.Where(predicate).FirstOrDefault();
    }

    public async Task<Models.Goal> SelectAsync(Expression<Func<Models.Goal, bool>> predicate)
    {
        return await dbContext.Goals.Where(predicate).FirstOrDefaultAsync();
    }
}