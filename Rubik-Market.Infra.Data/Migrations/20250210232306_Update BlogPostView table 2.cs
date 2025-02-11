using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rubik_Market.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlogPostViewtable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewDate",
                table: "BolgPostViews");

            migrationBuilder.UpdateData(
                table: "AboutUsDescription",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 2, 11, 2, 53, 4, 521, DateTimeKind.Local).AddTicks(1382));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ViewDate",
                table: "BolgPostViews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AboutUsDescription",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 2, 11, 2, 51, 9, 182, DateTimeKind.Local).AddTicks(1224));
        }
    }
}
