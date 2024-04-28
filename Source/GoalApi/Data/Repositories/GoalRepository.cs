﻿using System.Linq.Expressions;
using LS.Common;
using Microsoft.EntityFrameworkCore;

namespace GoalApi.Data.Repositories;

public class GoalRepository : IGenericRepository<Models.Goal>
{
    private readonly GoalDbContext _dbContext;

    public GoalRepository(GoalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Models.Goal> GetQuery()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Models.Goal> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<Models.Goal>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Models.Goal GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Models.Goal GetByIdWithIncludes(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Goal> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Goal> GetByIdWithIncludesAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public bool Remove(Guid id)
    {
        var goal = _dbContext.Goals.Find(id);
        if (goal is { })
        {
            _dbContext.Goals.Remove(goal);
            return true;
        }

        return false;
    }

    public void Add(in Models.Goal sender)
    {
        _dbContext.Add(sender).State = EntityState.Added;
    }

    public void Update(in Models.Goal sender)
    {
        _dbContext.Update(sender).State = EntityState.Modified;
    }

    public int Save()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveAsync()
    {
        throw new NotImplementedException();
    }

    public Models.Goal Select(Expression<Func<Models.Goal, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Goal> SelectAsync(Expression<Func<Models.Goal, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}