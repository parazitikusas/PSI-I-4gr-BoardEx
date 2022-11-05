using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardEx.Web.Migrations
{
    public partial class AddingFunctionalityForComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardAdComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoardAdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardAdComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardAdComment_BoardAds_BoardAdId",
                        column: x => x.BoardAdId,
                        principalTable: "BoardAds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardAdComment_BoardAdId",
                table: "BoardAdComment",
                column: "BoardAdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardAdComment");
        }
    }
}
