using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExpectedPingPeriod = table.Column<int>(nullable: false),
                    LastPing = table.Column<DateTime>(nullable: false),
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
