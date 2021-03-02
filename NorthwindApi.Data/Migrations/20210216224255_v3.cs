using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NorthwindApi.Data.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Customer");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Employees",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
