namespace api.Dtos;

public class RegisterDTO
{
    public string Email { get; set; } = null!;
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }

    public DateTime? CreateDateTime { get; set; } = DateTime.Now;
}