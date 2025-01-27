using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rubik_Market.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateFaqtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faqs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faqs", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "AboutUsDescription",
                columns: new[] { "ID", "AboutUsImageName", "AboutUsText", "CreateDate", "isDelete" },
                values: new object[] { 1, "DefaultImg", "Default", new DateTime(2025, 1, 27, 22, 29, 8, 917, DateTimeKind.Local).AddTicks(8266), false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faqs");

            migrationBuilder.DeleteData(
                table: "AboutUsDescription",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
