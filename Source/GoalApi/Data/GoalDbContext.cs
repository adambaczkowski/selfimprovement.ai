using GoalApi.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GoalApi.Data;

public class GoalDbContext : DbContext
{
    public GoalDbContext(
        DbContextOptions opt)
        : base(opt)
    {
    }
    
    public virtual DbSet<Models.Goal> Goals { get; set; }
    public virtual DbSet<Models.GoalTask> GoalTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Goal>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasMany<Models.GoalTask>()
                .WithOne(x => x.Goal)
                .HasForeignKey(x => x.GoalId);
            entity.Property(x => x.Category)
                .HasConversion<string>()
                .HasMaxLength(20);
            entity.Property(x => x.TimeAvailabilityPerWeek)
                .HasConversion<string>()
                .HasMaxLength(20);
            entity.Property(x => x.TimeAvailabilityPerDay)
                .HasConversion<string>()
                .HasMaxLength(20);
            entity.Property(x => x.Experience)
                .HasConversion<string>()
                .HasMaxLength(20);
            entity.Property(x => x.LearningType)
                .HasConversion<string>()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<Models.GoalTask>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasOne<Models.Goal>(x => x.Goal)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.GoalId);
        });
    }
}

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GoalDbContext>
{
    public GoalDbContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder();
        var conf = builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        return new GoalDbContext(
            new DbContextOptionsBuilder<GoalDbContext>().UseNpgsql(conf.GetConnectionString("SelfImprovementDbContext")).Options);
    }
}