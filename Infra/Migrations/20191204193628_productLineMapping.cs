using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
  public partial class productLineMapping : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "ProductLine",
          columns: table => new
          {
            Id = table.Column<Guid>(nullable: false),
            Name = table.Column<string>(nullable: false),
            ProductCategoryId = table.Column<Guid>(nullable: false),
            IsActive = table.Column<bool>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_ProductLine", x => x.Id);
            table.ForeignKey(
                      name: "FK_ProductLine_ProductCategory_ProductCategoryId",
                      column: x => x.ProductCategoryId,
                      principalTable: "ProductCategory",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_ProductLine_ProductCategoryId",
          table: "ProductLine",
          column: "ProductCategoryId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "ProductLine");
    }
  }
}
