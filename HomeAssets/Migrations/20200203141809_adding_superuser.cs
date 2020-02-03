using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAssets.Migrations
{
    public partial class adding_superuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Gender", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "suseradm-su01-9283-7465-k01joannes07", 0, "b682f5e6-e5d9-4ce9-986e-ce59fb6b9db9", "su@jdevops.xyz", true, 0, true, null, "SU@jdevops.xyz", "SUPERUSER", "AQAAAAEAACcQAAAAECSsynTcC+BpL/kCTy6r/9ncFzlORXGPO9RxAhNGIqdkibRzH7lpTzQ17Ii6KtmxGA==", null, false, "7d134ae2-c95c-4c72-a9d8-120db6b2c868", false, "superuser" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 999999999, "Role", "admin1", "suseradm-su01-9283-7465-k01joannes07" });
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
                keyValue: "suseradm-su01-9283-7465-k01joannes07");
        }
    }
}
