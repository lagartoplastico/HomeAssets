using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HomeAssets.Migrations
{
    public partial class authEmails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorizedEmails",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmailAddress = table.Column<string>(nullable: false),
                    DateOfCreation = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedEmails", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "suseradm-su01-9283-7465-k01joannes07",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0f60693-14ec-4bdb-8a62-63a7c9b0ee3a", "AQAAAAEAACcQAAAAEAZ6kxmhtdvh0/8mImNpvlEyEipHT68wTuTQmUL0GDtuPhecFD9JmFr+fgd9bULQIQ==", "88e4ad8d-05c9-4fb2-801a-c764b887a07f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorizedEmails");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "suseradm-su01-9283-7465-k01joannes07",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b682f5e6-e5d9-4ce9-986e-ce59fb6b9db9", "AQAAAAEAACcQAAAAECSsynTcC+BpL/kCTy6r/9ncFzlORXGPO9RxAhNGIqdkibRzH7lpTzQ17Ii6KtmxGA==", "7d134ae2-c95c-4c72-a9d8-120db6b2c868" });
        }
    }
}
