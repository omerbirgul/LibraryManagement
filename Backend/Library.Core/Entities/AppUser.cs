using Microsoft.AspNetCore.Identity;

namespace Library.Core.Entities;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
    public bool IsApproved { get; set; } = false;
    public UserRefreshToken RefreshToken { get; set; }
    public ICollection<BookRental> Rentals { get; set; }
}