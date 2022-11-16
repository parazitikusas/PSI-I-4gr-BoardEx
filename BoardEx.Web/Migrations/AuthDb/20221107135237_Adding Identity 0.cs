using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardEx.Web.Migrations.AuthDb
{
    public partial class AddingIdentity0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d0b88151-2770-46fe-8db5-cf1f1d98091c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c1d012c-b025-46c0-aa76-dd9e95f801fb", "AQAAAAEAACcQAAAAEAZ2pfuqpCnXyxO6MKgSugvfHdS9hU2kN5cQ7LFBdNw0wP0LsDNCqK4bvw7ZBe28kw==", "8afc752f-a346-48cd-8426-c71efdb65817" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d0b88151-2770-46fe-8db5-cf1f1d98091c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fdd2540-42ce-4698-a3e8-9b1f1908bae5", "AQAAAAEAACcQAAAAEN5hDSgJIE5bHbaUmf6Zx3ut/rucdrrd9pxZXX4jfbZBPt1BP08L4WQVTDjR5FeBSg==", "6584ae9c-b7f7-4ec7-bd6e-cc4ef4391f89" });
        }
    }
}
