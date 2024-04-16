using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PromptApi.Data;

public class PromptDbContext : DbContext
{
    public PromptDbContext(
        DbContextOptions opt)
        : base(opt)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        
    }
}