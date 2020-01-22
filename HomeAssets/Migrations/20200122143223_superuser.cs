using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAssets.Migrations
{
    public partial class superuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Gender", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "susususu-su01-9283-7465-001abcdetrn5", 0, "c1f68159-5450-4b76-9057-7e2d0cf6c822", "superuser@superuser.local", true, 0, false, null, "SUPERUSER@SUPERUSER.LOCAL", "SUPERUSER", "AQAAAAEAACcQAAAAEP9XRASyYOoHrTjAl8zLJfHJ9TOxenzsBEaDkCT6IPCi5d2qJvrGJyKDdwt43LxKZg==", null, false, "349fc654-5fcd-49f2-b62a-51d392a18e20", false, "superuser" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 999999999, "Role", "Administrador CON permisos de modificación", "susususu-su01-9283-7465-001abcdetrn5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 999999999);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "susususu-su01-9283-7465-001abcdetrn5");
        }
    }
}
