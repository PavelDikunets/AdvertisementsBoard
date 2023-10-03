using AdvertisementsBoard.Domain.SubCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.SubCategories.Configurations;

/// <summary>
///     Конфигурация таблицы Categories.
/// </summary>
public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.ToTable("SubCategories");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

        builder.HasMany(p => p.Advertisements)
            .WithOne(b => b.SubCategory)
            .HasForeignKey(b => b.SubCategoryId).IsRequired();
    }
}