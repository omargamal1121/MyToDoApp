using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_Do_List.Migrations
{
    /// <inheritdoc />
    public partial class adduseridtoMainCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MainCategory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MainCategory_UserId",
                table: "MainCategory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MainCategory_AspNetUsers_UserId",
                table: "MainCategory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainCategory_AspNetUsers_UserId",
                table: "MainCategory");

            migrationBuilder.DropIndex(
                name: "IX_MainCategory_UserId",
                table: "MainCategory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MainCategory");
        }
    }
}
