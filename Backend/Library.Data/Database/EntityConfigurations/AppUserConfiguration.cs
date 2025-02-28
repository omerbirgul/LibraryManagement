using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Database.EntityConfigurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasMany(u => u.Rentals) // Bir user birden fazla kiralama yapabilir.
            .WithOne(r => r.User) // Her kiralama yalnız tek bir user'a aittir.
            .HasForeignKey(r => r.UserId) // User-Rental arasında foreign key belirledik.
            .OnDelete(DeleteBehavior.Cascade); // User silinirse rental verileri de silinir.
    }
}