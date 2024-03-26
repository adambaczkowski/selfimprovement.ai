using IdentityApi.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Data;

public class IdentityDbContext : IdentityDbContext<Models.User>
{
    public IdentityDbContext(
        DbContextOptions opt)
        : base(opt)
    {
    }
    public virtual DbSet<UserProfile> UserProfiles { get; set; }
    public virtual DbSet<Models.Goal> Goals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.User>(entity =>
        {
            entity.HasMany<Models.Goal>()
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            entity.HasOne<UserProfile>()
                .WithOne(x => x.User)
                .HasForeignKey<Models.User>(x => x.UserProfileId);
        });

        modelBuilder.Entity<Models.Goal>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasMany<GoalTask>()
                .WithOne(x => x.Goal)
                .HasForeignKey(x => x.GoalId);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(x => x.Id);
        });
    }
}