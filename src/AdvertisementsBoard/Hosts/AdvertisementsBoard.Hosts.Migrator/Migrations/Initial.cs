#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertisementsBoard.Hosts.Migrator.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Categories",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Name = table.Column<string>("character varying(30)", maxLength: 30, nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Categories", x => x.Id); });

        migrationBuilder.CreateTable(
            "Users",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Name = table.Column<string>("character varying(15)", maxLength: 15, nullable: true),
                PhoneNumber = table.Column<string>("character varying(18)", maxLength: 18, nullable: true),
                Role = table.Column<int>("integer", nullable: false),
                NickName = table.Column<string>("character varying(20)", maxLength: 20, nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

        migrationBuilder.CreateTable(
            "SubCategories",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Name = table.Column<string>("character varying(30)", maxLength: 30, nullable: false),
                CategoryId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SubCategories", x => x.Id);
                table.ForeignKey(
                    "FK_SubCategories_Categories_CategoryId",
                    x => x.CategoryId,
                    "Categories",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Accounts",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Email = table.Column<string>("character varying(50)", maxLength: 50, nullable: false),
                PasswordHash = table.Column<string>("character varying(64)", maxLength: 64, nullable: false),
                Created = table.Column<DateTime>("timestamp with time zone", nullable: false),
                IsBlocked = table.Column<bool>("boolean", nullable: false),
                UserId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Accounts", x => x.Id);
                table.ForeignKey(
                    "FK_Accounts_Users_UserId",
                    x => x.UserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Advertisements",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Title = table.Column<string>("character varying(60)", maxLength: 60, nullable: false),
                Description = table.Column<string>("character varying(500)", maxLength: 500, nullable: false),
                Price = table.Column<decimal>("numeric(12)", precision: 12, nullable: false),
                TagNames = table.Column<string[]>("text[]", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                SubCategoryId = table.Column<Guid>("uuid", nullable: false),
                UserId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Advertisements", x => x.Id);
                table.ForeignKey(
                    "FK_Advertisements_SubCategories_SubCategoryId",
                    x => x.SubCategoryId,
                    "SubCategories",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_Advertisements_Users_UserId",
                    x => x.UserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Attachments",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Url = table.Column<string>("character varying(50)", maxLength: 50, nullable: false),
                AdvertisementId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Attachments", x => x.Id);
                table.ForeignKey(
                    "FK_Attachments_Advertisements_AdvertisementId",
                    x => x.AdvertisementId,
                    "Advertisements",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Comments",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Text = table.Column<string>("character varying(100)", maxLength: 100, nullable: false),
                Created = table.Column<DateTime>("timestamp with time zone", nullable: false),
                UserId = table.Column<Guid>("uuid", nullable: false),
                AdvertisementId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Comments", x => x.Id);
                table.ForeignKey(
                    "FK_Comments_Advertisements_AdvertisementId",
                    x => x.AdvertisementId,
                    "Advertisements",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_Comments_Users_UserId",
                    x => x.UserId,
                    "Users",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            "Users",
            new[] { "Id", "Name", "NickName", "PhoneNumber", "Role" },
            new object[]
                { new Guid("f7ed49f2-467a-4ae0-83b1-fd3c78d1ebb5"), "Administrator", "Administrator", null, 2 });

        migrationBuilder.InsertData(
            "Accounts",
            new[] { "Id", "Created", "Email", "IsBlocked", "PasswordHash", "UserId" },
            new object[]
            {
                new Guid("f91276a9-01a2-4dc9-bd65-f983c4b8e39d"),
                new DateTime(2023, 11, 15, 22, 24, 0, 481, DateTimeKind.Utc).AddTicks(3965), "admin@admin.com", false,
                "d82494f05d6917ba02f7aaa29689ccb444bb73f20380876cb05d1f37537b7892",
                new Guid("f7ed49f2-467a-4ae0-83b1-fd3c78d1ebb5")
            });

        migrationBuilder.CreateIndex(
            "IX_Accounts_UserId",
            "Accounts",
            "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Advertisements_SubCategoryId",
            "Advertisements",
            "SubCategoryId");

        migrationBuilder.CreateIndex(
            "IX_Advertisements_UserId",
            "Advertisements",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_Attachments_AdvertisementId",
            "Attachments",
            "AdvertisementId");

        migrationBuilder.CreateIndex(
            "IX_Comments_AdvertisementId",
            "Comments",
            "AdvertisementId");

        migrationBuilder.CreateIndex(
            "IX_Comments_UserId",
            "Comments",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_SubCategories_CategoryId",
            "SubCategories",
            "CategoryId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Accounts");

        migrationBuilder.DropTable(
            "Attachments");

        migrationBuilder.DropTable(
            "Comments");

        migrationBuilder.DropTable(
            "Advertisements");

        migrationBuilder.DropTable(
            "SubCategories");

        migrationBuilder.DropTable(
            "Users");

        migrationBuilder.DropTable(
            "Categories");
    }
}