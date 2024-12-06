using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Arahk.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterSalutationinUserProfilestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Salutations_SalutationId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_SalutationId",
                table: "UserProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salutations",
                table: "Salutations");

            migrationBuilder.DeleteData(
                table: "Salutations",
                keyColumn: "Name",
                keyValue: "Mr");

            migrationBuilder.DeleteData(
                table: "Salutations",
                keyColumn: "Name",
                keyValue: "Mrs");

            migrationBuilder.DeleteData(
                table: "Salutations",
                keyColumn: "Name",
                keyValue: "Ms");

            migrationBuilder.DropColumn(
                name: "SalutationId",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "Salutation",
                table: "UserProfiles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Salutations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Salutations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salutations",
                table: "Salutations",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Salutations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1030d779-5c7f-489f-a473-516ca163e2ca"), "Mrs" },
                    { new Guid("1e60b16c-f3a6-4029-af97-8e644c158d3b"), "Mr" },
                    { new Guid("8164009b-952e-4f0b-822e-fb40f1bd08f2"), "Ms" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Salutations",
                table: "Salutations");

            migrationBuilder.DeleteData(
                table: "Salutations",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("1030d779-5c7f-489f-a473-516ca163e2ca"));

            migrationBuilder.DeleteData(
                table: "Salutations",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("1e60b16c-f3a6-4029-af97-8e644c158d3b"));

            migrationBuilder.DeleteData(
                table: "Salutations",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("8164009b-952e-4f0b-822e-fb40f1bd08f2"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Salutations");

            migrationBuilder.AlterColumn<string>(
                name: "Salutation",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "SalutationId",
                table: "UserProfiles",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Salutations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salutations",
                table: "Salutations",
                column: "Name");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Salutations_SalutationId",
                table: "UserProfiles",
                column: "SalutationId",
                principalTable: "Salutations",
                principalColumn: "Name");
        }
    }
}
