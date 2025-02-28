using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Database.EntityConfigurations;

public class BookRentalConfiguration : IEntityTypeConfiguration<BookRental>
{
    public void Configure(EntityTypeBuilder<BookRental> builder)
    {
        builder.HasKey(br => br.Id);

        builder.Property(br => br.ReturnDate)
            .IsRequired(false);

        builder.HasOne(br => br.User) // Her kiralama sadece bir kullanıcı'ya aittir
            .WithMany(u => u.Rentals) // Ancak bir kullanıcı birden fazla kiralama yapabilir.
            .HasForeignKey(br => br.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(br => br.Book) // Her kiralama sadece bir kitaba aittir.
            .WithMany(b => b.Rentals) // Ancak bir kitabın birden fazla kiralama kaydına sahip olabilir.
            .HasForeignKey(br => br.BookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}