using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ITestApp.Data.Migrations
{
    public partial class UserTestChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Points",
                table: "UserTests",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartOn",
                table: "UserTests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedOn",
                table: "UserTests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartOn",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "SubmittedOn",
                table: "UserTests");

            migrationBuilder.AlterColumn<float>(
                name: "Points",
                table: "UserTests",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);
        }
    }
}
