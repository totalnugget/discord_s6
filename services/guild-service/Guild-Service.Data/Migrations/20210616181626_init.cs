using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GuildService.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guild",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guild", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChannelPos",
                columns: table => new
                {
                    ChannelId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    GuildId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelPos", x => x.ChannelId);
                    table.ForeignKey(
                        name: "FK_ChannelPos_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuildUsers",
                columns: table => new
                {
                    GuildId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Nickname = table.Column<string>(nullable: false),
                    Permissions = table.Column<int>(nullable: false),
                    IsOwner = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildUsers", x => new { x.GuildId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GuildUsers_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Guild",
                columns: new[] { "Id", "CreatedAt", "Description", "LastUpdatedAt", "Name", "Region" },
                values: new object[] { 1, new DateTime(2021, 6, 16, 18, 16, 25, 678, DateTimeKind.Utc).AddTicks(2618), "talk en play zone for friends", new DateTime(2021, 6, 16, 18, 16, 25, 678, DateTimeKind.Utc).AddTicks(2618), "club house", "eu" });

            migrationBuilder.InsertData(
                table: "ChannelPos",
                columns: new[] { "ChannelId", "GuildId", "Name", "Position" },
                values: new object[] { 1, 1, "general", 1 });

            migrationBuilder.InsertData(
                table: "GuildUsers",
                columns: new[] { "GuildId", "UserId", "IsOwner", "Nickname", "Permissions" },
                values: new object[] { 1, 1, true, "harry lotions :)", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelPos_GuildId",
                table: "ChannelPos",
                column: "GuildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelPos");

            migrationBuilder.DropTable(
                name: "GuildUsers");

            migrationBuilder.DropTable(
                name: "Guild");
        }
    }
}
