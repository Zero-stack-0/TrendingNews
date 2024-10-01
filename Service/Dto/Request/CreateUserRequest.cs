namespace Service.Dto;

public class CreateUserRequest
{
    public required string UserName { get; set; }
    public required string EmailId { get; set; }
    public required string PassWord { get; set; }
}