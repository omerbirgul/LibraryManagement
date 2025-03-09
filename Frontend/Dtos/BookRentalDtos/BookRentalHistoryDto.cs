namespace Library.Mvc.Dtos.BookRentalDtos
{
    public class BookRentalHistoryDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string ISBN { get; set; }
        public string RentedBy { get; set; }
        public string UserId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}
