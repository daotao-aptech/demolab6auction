using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuctionSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    StartPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrentPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuctionItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    BidderName = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_AuctionItems_AuctionItemId",
                        column: x => x.AuctionItemId,
                        principalTable: "AuctionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AuctionItems",
                columns: new[] { "Id", "CurrentPrice", "Description", "EndTime", "StartPrice", "Title" },
                values: new object[,]
                {
                    { 1, 50.00m, "An old vintage clock.", new DateTime(2025, 9, 15, 19, 54, 11, 34, DateTimeKind.Local).AddTicks(6903), 50.00m, "Vintage Clock" },
                    { 2, 100.00m, "A beautiful antique vase.", new DateTime(2025, 9, 13, 19, 54, 11, 34, DateTimeKind.Local).AddTicks(6927), 100.00m, "Antique Vase" }
                });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "Amount", "AuctionItemId", "BidderName", "CreatedAt" },
                values: new object[,]
                {
                    { 1, 60.00m, 1, "Alice", new DateTime(2025, 9, 8, 17, 54, 11, 34, DateTimeKind.Local).AddTicks(7227) },
                    { 2, 70.00m, 1, "Bob", new DateTime(2025, 9, 8, 18, 54, 11, 34, DateTimeKind.Local).AddTicks(7231) },
                    { 3, 120.00m, 2, "Charlie", new DateTime(2025, 9, 8, 16, 54, 11, 34, DateTimeKind.Local).AddTicks(7296) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_AuctionItemId",
                table: "Bids",
                column: "AuctionItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "AuctionItems");
        }
    }
}
