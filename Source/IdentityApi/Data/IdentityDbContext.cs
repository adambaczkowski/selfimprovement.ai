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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.User>(entity =>
        {
            entity.HasOne<UserProfile>()
                .WithOne(x => x.User)
                .HasForeignKey<Models.User>(x => x.UserProfileId);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(x => x.Id);
        });
    }
}