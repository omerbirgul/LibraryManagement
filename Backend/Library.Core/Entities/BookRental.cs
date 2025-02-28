namespace Library.Core.Entities;

public class BookRental
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; }

    public DateTime RentalDate { get; set; } = DateTime.Now;
    public DateTime? ReturnDate { get; set; }
}