using AdvertisementsBoard.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Categories.Configurations;

/// <summary>
///     Конфигурация таблицы Categories.
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Name).HasMaxLength(30).IsRequired();

        builder.HasMany(p => p.SubCategories)
            .WithOne(b => b.Category)
            .HasForeignKey(b => b.CategoryId);
    }
}