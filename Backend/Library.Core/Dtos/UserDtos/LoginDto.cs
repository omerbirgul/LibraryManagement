namespace Library.Core.Dtos.UserDtos;

// public record LoginDto(string Email, string Password);

public class LoginDto
{
    
    // kullanıcı sisteme giriş yaptığında email ve password eşleşirse kullanıcıya geri token döner.
    // ClientId = Email ya da Username
    // ClientSecret = Password
    public string Email { get; set; }
    public string Password { get; set; }
}