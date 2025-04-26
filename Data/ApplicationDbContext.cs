using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext:IdentityDbContext<AppUser,IdentityRole<Guid>,Guid>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
}