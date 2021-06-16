using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UserService.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    KeycloakGUID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "KeycloakGUID", "Name", "LastUpdatedAt" },
                values: new object[] { 1, new DateTime(2021, 6, 10, 22, 16, 47, 551, DateTimeKind.Local).AddTicks(1544), new Guid("cf9f68cb-78f0-4dd4-b203-8e520b422374"), "harry", new DateTime(2021, 6, 10, 22, 16, 47, 551, DateTimeKind.Local).AddTicks(1544) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
