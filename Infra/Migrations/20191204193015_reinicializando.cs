using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
  public partial class reinicializando : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Address",
          columns: table => new
          {
            Id = table.Column<Guid>(nullable: false),
            Street = table.Column<string>(nullable: true),
            HouseNumber = table.Column<int>(nullable: false),
            Neighborhood = table.Column<string>(nullable: true),
            City = table.Column<string>(nullable: true),
            State = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Address", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "User",
          columns: table => new
          {
            Id = table.Column<Guid>(nullable: false),
            Name = table.Column<string>(nullable: false),
            Login = table.Column<string>(nullable: false),
            Password = table.Column<string>(nullable: false),
            IsActive = table.Column<bool>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_User", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Supplier",
          columns: table => new
          {
            Id = table.Column<Guid>(nullable: false),
            CorporateName = table.Column<string>(nullable: false),
            cpnj = table.Column<string>(nullable: false),
            TradingName = table.Column<string>(nullable: false),
            AddressId = table.Column<Guid>(nullable: true),
            Email = table.Column<string>(nullable: false),
            Telephone = table.Column<string>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Supplier", x => x.Id);
            table.ForeignKey(
                      name: "FK_Supplier_Address_AddressId",
                      column: x => x.AddressId,
                      principalTable: "Address",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
          });

      migrationBuilder.CreateTable(
          name: "ProductCategory",
          columns: table => new
          {
            Id = table.Column<Guid>(nullable: false),
            CategoryName = table.Column<string>(nullable: false),
            SupplierId = table.Column<Guid>(nullable: false),
            IsActive = table.Column<bool>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_ProductCategory", x => x.Id);
            table.ForeignKey(
                      name: "FK_ProductCategory_Supplier_SupplierId",
                      column: x => x.SupplierId,
                      principalTable: "Supplier",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_ProductCategory_SupplierId",
          table: "ProductCategory",
          column: "SupplierId");

      migrationBuilder.CreateIndex(
          name: "IX_Supplier_AddressId",
          table: "Supplier",
          column: "AddressId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "ProductCategory");

      migrationBuilder.DropTable(
          name: "User");

      migrationBuilder.DropTable(
          name: "Supplier");

      migrationBuilder.DropTable(
          name: "Address");
    }
  }
}
