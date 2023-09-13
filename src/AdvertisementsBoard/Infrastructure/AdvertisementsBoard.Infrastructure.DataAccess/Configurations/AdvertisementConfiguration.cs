using AdvertisementsBoard.Domain.Advertisements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Configurations;

/// <summary>
///     Конфигурация таблицы Advertisement.
/// </summary>
public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.ToTable(nameof(Advertisement));
        builder.Property(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
    }
}