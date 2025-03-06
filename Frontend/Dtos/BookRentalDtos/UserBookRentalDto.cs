namespace Library.Mvc.Dtos.BookRentalDtos
{
    public class UserBookRentalDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string ISBN { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
