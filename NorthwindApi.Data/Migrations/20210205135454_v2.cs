using Microsoft.EntityFrameworkCore.Migrations;

namespace NorthwindApi.Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fax",
                table: "Customer",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Customer",
                newName: "Fax");
        }
    }
}
