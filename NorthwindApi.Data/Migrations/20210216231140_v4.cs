using Microsoft.EntityFrameworkCore.Migrations;

namespace NorthwindApi.Data.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippers_ShipperId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ShipperId",
                table: "Orders",
                newName: "ShipVia");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ShipperId",
                table: "Orders",
                newName: "IX_Orders_ShipVia");

            migrationBuilder.AlterColumn<string>(
                name: "ShipPostalCode",
                table: "Orders",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "ShipCountry",
                table: "Orders",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippers_ShipVia",
                table: "Orders",
                column: "ShipVia",
                principalTable: "Shippers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippers_ShipVia",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ShipVia",
                table: "Orders",
                newName: "ShipperId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ShipVia",
                table: "Orders",
                newName: "IX_Orders_ShipperId");

            migrationBuilder.AlterColumn<string>(
                name: "ShipPostalCode",
                table: "Orders",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "ShipCountry",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippers_ShipperId",
                table: "Orders",
                column: "ShipperId",
                principalTable: "Shippers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
