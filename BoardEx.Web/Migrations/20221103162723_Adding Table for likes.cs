using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardEx.Web.Migrations
{
    public partial class AddingTableforlikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardAdLike",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoardAdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardAdLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardAdLike_BoardAds_BoardAdId",
                        column: x => x.BoardAdId,
                        principalTable: "BoardAds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardAdLike_BoardAdId",
                table: "BoardAdLike",
                column: "BoardAdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardAdLike");
        }
    }
}
