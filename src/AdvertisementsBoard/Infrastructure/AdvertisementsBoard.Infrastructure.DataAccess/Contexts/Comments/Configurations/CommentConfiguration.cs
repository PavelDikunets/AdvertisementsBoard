using AdvertisementsBoard.Domain.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Comments.Configurations;

/// <summary>
///     Конфигурация таблицы Categories.
/// </summary>
public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Text).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Created).HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc))
            .IsRequired();

        builder.HasOne(p => p.User)
            .WithMany(b => b.Comments)
            .HasForeignKey(b => b.UserId);

        builder.HasOne(p => p.Advertisement)
            .WithMany(c => c.Comments)
            .HasForeignKey(k => k.AdvertisementId);
    }
}