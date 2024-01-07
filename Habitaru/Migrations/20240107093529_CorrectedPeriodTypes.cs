using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habitaru.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedPeriodTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgStreakPeriod",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "MaxStreakPeriod",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "MinStreakPeriod",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "PrevStreakPeriod",
                table: "Habits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AvgStreakPeriod",
                table: "Habits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MaxStreakPeriod",
                table: "Habits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MinStreakPeriod",
                table: "Habits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PrevStreakPeriod",
                table: "Habits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
