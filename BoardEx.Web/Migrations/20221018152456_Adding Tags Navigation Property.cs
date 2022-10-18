using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardEx.Web.Migrations
{
    public partial class AddingTagsNavigationProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlHandler",
                table: "BoardAds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_BoardAdId",
                table: "Tags",
                column: "BoardAdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_BoardAds_BoardAdId",
                table: "Tags",
                column: "BoardAdId",
                principalTable: "BoardAds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_BoardAds_BoardAdId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_BoardAdId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UrlHandler",
                table: "BoardAds");
        }
    }
}
