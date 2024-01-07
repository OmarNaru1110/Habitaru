using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habitaru.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedPeriodTypesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvgStreakPeriod",
                table: "Habits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxStreakPeriod",
                table: "Habits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinStreakPeriod",
                table: "Habits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrevStreakPeriod",
                table: "Habits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
