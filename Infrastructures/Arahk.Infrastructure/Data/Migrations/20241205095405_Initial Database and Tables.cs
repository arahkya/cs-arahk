using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arahk.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseandTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salutations",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salutations", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Salutation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    PrimaryMobilePhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserIdentityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalutationId = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Salutations_SalutationId",
                        column: x => x.SalutationId,
                        principalTable: "Salutations",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserIdentityId",
                        column: x => x.UserIdentityId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Salutations",
                column: "Name",
                values: new object[]
                {
                    "Mr",
                    "Mrs",
                    "Ms"
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_SalutationId",
                table: "UserProfiles",
                column: "SalutationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserIdentityId",
                table: "UserProfiles",
                column: "UserIdentityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Salutations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
