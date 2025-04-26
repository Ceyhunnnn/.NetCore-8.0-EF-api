namespace api.Dtos;

public class ChangePasswordDTO
{
    public Guid id { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
