using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Medium.ReplacingIfElse.DataLayer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Application",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Address_StreetName = table.Column<string>(maxLength: 150, nullable: true),
                    Address_StreetNumber = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.InsertData(
                schema: "Application",
                table: "Users",
                columns: new[] { "id", "Email", "Username" },
                values: new object[] { new Guid("be20ff2c-d1e6-47d4-8a2c-40cd0e528c6c"), "someuser@user.dk", "someuser@user.dk" });

            migrationBuilder.InsertData(
                schema: "Application",
                table: "Users",
                columns: new[] { "id", "Email", "Username" },
                values: new object[] { new Guid("7d0cc1c1-f97c-479e-9df2-560c8293e934"), "otheruser@user.dk", "otheruser@user.dk" });

            migrationBuilder.InsertData(
                schema: "Application",
                table: "Users",
                columns: new[] { "id", "Email", "Username" },
                values: new object[] { new Guid("e8f2bce5-c55f-4965-89f3-71439ffffe3a"), "lastuser@user.dk", "lastuser@user.dk" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "Application",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "Application");
        }
    }
}
