using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeesPortal.Shared.Migrations
{
    /// <inheritdoc />
    public partial class upsch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleStatusId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleStatusId",
                table: "Schedules");
        }
    }
}
