using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsThisGeekAlive.Migrations
{
    public partial class RenamingLoginColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPingLocalTime",
                table: "Geeks");

            migrationBuilder.DropColumn(
                name: "LastPingServerTime",
                table: "Geeks");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastActivityLocalTime",
                table: "Geeks",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastActivityServerTime",
                table: "Geeks",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastActivityLocalTime",
                table: "Geeks");

            migrationBuilder.DropColumn(
                name: "LastActivityServerTime",
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
        }
    }
}
