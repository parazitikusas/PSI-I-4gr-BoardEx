using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardEx.Web.Migrations.AuthDb
{
    public partial class Mantodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d0b88151-2770-46fe-8db5-cf1f1d98091c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a58b4ae-9405-4b82-b6bc-843fee3adcd9", "AQAAAAEAACcQAAAAECnTH31doMGk/X7fTstWaetF4FJP3wTkLvv+Alw06hQqGEjUszalDuWDtflL0cyARw==", "77270822-2e0d-44e7-b706-e4fb50ab97e5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d0b88151-2770-46fe-8db5-cf1f1d98091c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c1d012c-b025-46c0-aa76-dd9e95f801fb", "AQAAAAEAACcQAAAAEAZ2pfuqpCnXyxO6MKgSugvfHdS9hU2kN5cQ7LFBdNw0wP0LsDNCqK4bvw7ZBe28kw==", "8afc752f-a346-48cd-8426-c71efdb65817" });
        }
    }
}
