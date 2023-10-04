using AdvertisementsBoard.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Users.Configurations;

/// <summary>
/// Конфигурация таблицы Users.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Name).HasMaxLength(64).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Password).HasMaxLength(32).IsRequired();
    }
}