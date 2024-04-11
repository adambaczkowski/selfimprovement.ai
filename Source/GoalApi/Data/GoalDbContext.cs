using GoalApi.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoalApi.Data;

public class GoalDbContext : DbContext
{
    public GoalDbContext(
        DbContextOptions opt)
        : base(opt)
    {
    }
    
    public virtual DbSet<Models.Goal> Goals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Goal>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasMany<GoalTask>()
                .WithOne(x => x.Goal)
                .HasForeignKey(x => x.GoalId);
        });
    }
}