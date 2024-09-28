using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeesPortal.Shared.Migrations
{
    /// <inheritdoc />
    public partial class upsch2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOff",
                table: "Schedules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOff",
                table: "Schedules");
        }
    }
}
