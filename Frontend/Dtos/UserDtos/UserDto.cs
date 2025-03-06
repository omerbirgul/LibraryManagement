using Library.Mvc.Dtos.BookRentalDtos;

namespace Library.Mvc.Dtos.UserDtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public List<UserBookRentalDto> UserRentalHistory { get; set; }
    }
}
