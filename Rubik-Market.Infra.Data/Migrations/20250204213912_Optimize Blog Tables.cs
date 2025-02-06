using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rubik_Market.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class OptimizeBlogTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlogPostGroups_GroupId",
                table: "BlogPostGroups");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostGroups_PostId",
                table: "BlogPostGroups");

            migrationBuilder.UpdateData(
                table: "AboutUsDescription",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 2, 5, 1, 9, 7, 824, DateTimeKind.Local).AddTicks(2774));

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostGroups_GroupId",
                table: "BlogPostGroups",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostGroups_PostId",
                table: "BlogPostGroups",
                column: "PostId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlogPostGroups_GroupId",
                table: "BlogPostGroups");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostGroups_PostId",
                table: "BlogPostGroups");

            migrationBuilder.UpdateData(
                table: "AboutUsDescription",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 2, 3, 1, 20, 33, 917, DateTimeKind.Local).AddTicks(8134));

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostGroups_GroupId",
                table: "BlogPostGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostGroups_PostId",
                table: "BlogPostGroups",
                column: "PostId");
        }
    }
}
