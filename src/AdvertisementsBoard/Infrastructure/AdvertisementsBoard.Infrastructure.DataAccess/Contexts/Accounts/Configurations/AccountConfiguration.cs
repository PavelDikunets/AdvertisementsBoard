using AdvertisementsBoard.Domain.Accounts;
using AdvertisementsBoard.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Accounts.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Email).HasMaxLength(50).IsRequired();
        builder.Property(p => p.PasswordHash).HasMaxLength(64).IsRequired();
        builder.Property(p => p.Created).HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc))
            .IsRequired();
        builder.Property(p => p.IsBlocked).IsRequired();


        builder.HasOne(p => p.User)
            .WithOne(p => p.Account)
            .HasForeignKey<User>(p => p.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        var accountId = new Guid("F91276A9-01A2-4DC9-BD65-F983C4B8E39D");

        builder.HasData(
            new Account
            {
                Id = accountId,
                PasswordHash = "d82494f05d6917ba02f7aaa29689ccb444bb73f20380876cb05d1f37537b7892",
                Email = "admin@admin.com",
                IsBlocked = false,
                Created = DateTime.UtcNow
            });
    }
}