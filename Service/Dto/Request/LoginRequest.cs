namespace Service.Dto.Request;

public class LoginRequest
{
    public required string EmailOrUserName { get; set; }
    public required string Password { get; set; }
}