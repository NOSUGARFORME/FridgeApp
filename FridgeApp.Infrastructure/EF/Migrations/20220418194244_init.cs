using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridgeApp.Infrastructure.EF.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fridges");

            migrationBuilder.CreateTable(
                name: "FridgeModels",
                schema: "fridges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgeModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "fridges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DefaultQuantity = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fridges",
                schema: "fridges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    OwnerName = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    FridgeModelId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fridges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fridges_FridgeModels_FridgeModelId",
                        column: x => x.FridgeModelId,
                        principalSchema: "fridges",
                        principalTable: "FridgeModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FridgeProducts",
                schema: "fridges",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    FridgeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgeProducts", x => new { x.FridgeId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_FridgeProducts_Fridges_FridgeId",
                        column: x => x.FridgeId,
                        principalSchema: "fridges",
                        principalTable: "Fridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FridgeProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "fridges",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FridgeProducts_ProductId",
                schema: "fridges",
                table: "FridgeProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Fridges_FridgeModelId",
                schema: "fridges",
                table: "Fridges",
                column: "FridgeModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FridgeProducts",
                schema: "fridges");

            migrationBuilder.DropTable(
                name: "Fridges",
                schema: "fridges");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "fridges");

            migrationBuilder.DropTable(
                name: "FridgeModels",
                schema: "fridges");
        }
    }
}
