namespace Library.Core.Entities;

public class UserRefreshToken
{
    public int Id { get; set; }
    public AppUser User { get; set; }
    public string UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
}