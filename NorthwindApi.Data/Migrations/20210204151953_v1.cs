using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NorthwindApi.Data.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Picture = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    ContactName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    ContactTitle = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    City = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Country = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Phone = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    LastName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "Date", nullable: false),
                    HireDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Address = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    City = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Country = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    PhotoPath = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Phone = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    ContactName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    ContactTitle = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Adress = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    City = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Country = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Phone = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "Date", nullable: false),
                    RequiredDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ShipName = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    ShipAddress = table.Column<string>(type: "varchar(65)", maxLength: 65, nullable: false),
                    ShipCity = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    ShipPostalCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    ShipCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Shippers_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "Shippers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    SupplierID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityPerUnit = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal", nullable: false),
                    UnitsInStock = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    DisCount = table.Column<double>(type: "float", nullable: false),
                    ProductId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId1",
                table: "OrderDetails",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeID",
                table: "Orders",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipperId",
                table: "Orders",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierID",
                table: "Products",
                column: "SupplierID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Shippers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
