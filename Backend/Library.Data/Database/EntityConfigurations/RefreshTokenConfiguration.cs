using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Database.EntityConfigurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.ExpirationDate).IsRequired();
        builder.Property(t => t.Token).IsRequired();
        builder.Property(t => t.UserId).IsRequired();

        builder.HasOne(t => t.User)
            .WithOne(u => u.RefreshToken)
            .HasForeignKey<UserRefreshToken>(t => t.UserId);

    }
}