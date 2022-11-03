using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardEx.Web.Migrations.AuthDb
{
    public partial class Addingnormalizedusername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d0b88151-2770-46fe-8db5-cf1f1d98091c",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fdd2540-42ce-4698-a3e8-9b1f1908bae5", "SUPERADMIN@SUPERADMIN", "SUPERADMIN@SUPERADMIN", "AQAAAAEAACcQAAAAEN5hDSgJIE5bHbaUmf6Zx3ut/rucdrrd9pxZXX4jfbZBPt1BP08L4WQVTDjR5FeBSg==", "6584ae9c-b7f7-4ec7-bd6e-cc4ef4391f89" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d0b88151-2770-46fe-8db5-cf1f1d98091c",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5f3038e-5999-4e1e-847e-6403024a137b", null, null, "AQAAAAEAACcQAAAAEE9WmIzdqcJmhww7YCL3G7EgLX9uZWtYmJ0yLe+1ufxHasJC0hx7uwiI6NQ33GpgUA==", "5fa8df6e-3033-4383-b307-10d40db7b722" });
        }
    }
}
