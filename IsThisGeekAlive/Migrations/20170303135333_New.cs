using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsThisGeekAlive.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedPingPeriod",
                table: "Geeks");

            migrationBuilder.DropColumn(
                name: "LastPing",
                table: "Geeks");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastPingLocalTime",
                table: "Geeks",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastPingServerTime",
                table: "Geeks",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "NotAliveDangerWindow",
                table: "Geeks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NotAliveWarningWindow",
                table: "Geeks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPingLocalTime",
                table: "Geeks");

            migrationBuilder.DropColumn(
                name: "LastPingServerTime",
                table: "Geeks");

            migrationBuilder.DropColumn(
                name: "NotAliveDangerWindow",
                table: "Geeks");

            migrationBuilder.DropColumn(
                name: "NotAliveWarningWindow",
                table: "Geeks");

            migrationBuilder.AddColumn<int>(
                name: "ExpectedPingPeriod",
                table: "Geeks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPing",
                table: "Geeks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
