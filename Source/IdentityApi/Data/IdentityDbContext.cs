using IdentityApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Data;

public class IdentityDbContext : IdentityDbContext<User>
{
    public IdentityDbContext(
        DbContextOptions<IdentityDbContext> opt)
        : base(opt)
    {
    }
    
    
}