using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habitaru.Migrations
{
    /// <inheritdoc />
    public partial class AddedHabitModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Habits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResetCount = table.Column<int>(type: "int", nullable: false),
                    FirstStreakDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurStreakDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxStreakPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinStreakPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvgStreakPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrevStreakPeriod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Habits");
        }
    }
}
