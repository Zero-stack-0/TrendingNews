namespace Entities;

public class Users
{
    public Users()
    { }

    public Users(string userName, string emailId, string passWord)
    {
        UserName = userName;
        EmailId = emailId;
        PassWord = passWord;
        CreatedAt = DateTime.UtcNow;
    }
    public long Id { get; set; }
    public string UserName { get; set; }
    public string EmailId { get; set; }
    public string PassWord { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}