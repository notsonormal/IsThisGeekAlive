using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsThisGeekAlive.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Geeks",
                columns: table => new
                {
                    GeekId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    LastActivityLocalTime = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    LastActivityLocalTimeUtcOffset = table.Column<short>(nullable: false),
                    LastActivityServerTime = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    LastActivityServerTimeUtcOffset = table.Column<short>(nullable: false),
                    LoginCode = table.Column<string>(maxLength: 100, nullable: false),
                    NotAliveDangerWindow = table.Column<int>(nullable: false),
                    NotAliveWarningWindow = table.Column<int>(nullable: false),
                    Username = table.Column<string>(maxLength: 500, nullable: false),
                    UsernameLower = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geeks", x => x.GeekId);
                });

            migrationBuilder.CreateIndex(
                name: "Geek_UsernameLower",
                table: "Geeks",
                column: "UsernameLower");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Geeks");
        }
    }
}
