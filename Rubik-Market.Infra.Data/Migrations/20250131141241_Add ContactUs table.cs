using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rubik_Market.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContactUstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "AboutUsDescription",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 1, 31, 17, 42, 33, 487, DateTimeKind.Local).AddTicks(4673));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.UpdateData(
                table: "AboutUsDescription",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 1, 28, 23, 57, 24, 720, DateTimeKind.Local).AddTicks(3049));
        }
    }
}
