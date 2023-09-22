using AdvertisementsBoard.Domain.Attachments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Attachments.Configurations;

/// <summary>
///     Конфигурация таблицы Attachment.
/// </summary>
public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachments");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.FileName).IsRequired().HasMaxLength(255);

        builder.HasOne(d => d.Advertisement)
            .WithMany(p => p.Attachments)
            .HasForeignKey(d => d.AdvertisementId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}