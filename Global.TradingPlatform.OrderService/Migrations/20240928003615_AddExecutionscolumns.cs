using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global.TradingPlatform.OrderService.Migrations
{
    /// <inheritdoc />
    public partial class AddExecutionscolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Executions",
                newName: "OrderQty");

            migrationBuilder.RenameColumn(
                name: "ExecutionTime",
                table: "Executions",
                newName: "OperationTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "OperationTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CumulativeQty",
                table: "Executions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastShares",
                table: "Executions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeavesQuantity",
                table: "Executions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OperationNumber",
                table: "Executions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Executions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CumulativeQty",
                table: "Executions");

            migrationBuilder.DropColumn(
                name: "LastShares",
                table: "Executions");

            migrationBuilder.DropColumn(
                name: "LeavesQuantity",
                table: "Executions");

            migrationBuilder.DropColumn(
                name: "OperationNumber",
                table: "Executions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Executions");

            migrationBuilder.RenameColumn(
                name: "OrderQty",
                table: "Executions",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "OperationTime",
                table: "Executions",
                newName: "ExecutionTime");
        }
    }
}
