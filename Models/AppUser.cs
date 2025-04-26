using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class AppUser : IdentityUser<Guid>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string FullName => string.Join(" ", FirstName, LastName);
}