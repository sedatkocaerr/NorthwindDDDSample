using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NorthwindApi.Data.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippers_ShipVia",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShipVia",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "ShipperId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipperId",
                table: "Orders",
                column: "ShipperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippers_ShipperId",
                table: "Orders",
                column: "ShipperId",
                principalTable: "Shippers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippers_ShipperId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShipperId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipperId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipVia",
                table: "Orders",
                column: "ShipVia");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippers_ShipVia",
                table: "Orders",
                column: "ShipVia",
                principalTable: "Shippers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
