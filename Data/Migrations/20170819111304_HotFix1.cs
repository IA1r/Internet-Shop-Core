using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Migrations
{
    public partial class HotFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSubType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductSubType",
                columns: table => new
                {
                    ProductSubTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductTypeID = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubType", x => x.ProductSubTypeID);
                    table.ForeignKey(
                        name: "FK_ProductSubType_ProductType_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductType",
                        principalColumn: "ProductTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubType_ProductTypeID",
                table: "ProductSubType",
                column: "ProductTypeID");
        }
    }
}
