using IdentityApi.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityApi.Data;

public class IdentityDbContext : IdentityDbContext<Models.User>
{
    public IdentityDbContext(
        DbContextOptions opt)
        : base(opt)
    {
    }
    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.User>(entity =>
        {
            entity.HasOne<UserProfile>(x => x.UserProfile)
                .WithOne(x => x.User)
                .HasForeignKey<Models.User>(x => x.UserProfileId);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasOne<Models.User>(x => x.User)
                .WithOne(x => x.UserProfile)
                .HasForeignKey<UserProfile>(x => x.UserId);
        });
    }
    
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder();
            var conf = builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return new IdentityDbContext(
                new DbContextOptionsBuilder<IdentityDbContext>().UseNpgsql(conf.GetConnectionString("SelfImprovementDbContext")).Options);
        }
    }
}