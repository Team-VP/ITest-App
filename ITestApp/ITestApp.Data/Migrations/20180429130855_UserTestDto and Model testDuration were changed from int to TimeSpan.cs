using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ITestApp.Data.Migrations
{
    public partial class UserTestDtoandModeltestDurationwerechangedfrominttoTimeSpan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ExecutionTime",
                table: "UserTests",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ExecutionTime",
                table: "UserTests",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }
    }
}
