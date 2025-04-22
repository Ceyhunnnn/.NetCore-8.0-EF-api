using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class TodoDbContext:DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options):base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public DbSet<TodoItem> TodoItems { get; set; }
    
}