namespace api.Dtos;

public class ChangePasswordUsingTokenDTO
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string NewPassword { get; set; }
    
}