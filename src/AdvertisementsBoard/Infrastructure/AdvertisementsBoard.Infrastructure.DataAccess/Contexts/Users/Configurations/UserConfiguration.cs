using AdvertisementsBoard.Common.Enums.Users;
using AdvertisementsBoard.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Users.Configurations;

/// <summary>
///     Конфигурация таблицы Users.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.NickName).HasMaxLength(20).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(15);
        builder.Property(p => p.Role).IsRequired();
        builder.Property(p => p.PhoneNumber).HasMaxLength(18);


        builder.HasMany(p => p.Advertisements)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(p => p.Account)
            .WithOne(p => p.User)
            .OnDelete(DeleteBehavior.Cascade);

        var accountId = new Guid("F91276A9-01A2-4DC9-BD65-F983C4B8E39D");

        builder.HasData(
            new User
            {
                Id = Guid.NewGuid(),
                Name = "Administrator",
                NickName = "Administrator",
                Role = UserRole.Administrator,
                AccountId = accountId
            });
    }
}