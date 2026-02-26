using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelItineraryApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItineraryDates2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dates",
                table: "Itineraries");

            migrationBuilder.AddColumn<int>(
                name: "ItineraryId",
                table: "Memories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Itineraries",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Itineraries",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Memories_ItineraryId",
                table: "Memories",
                column: "ItineraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memories_Itineraries_ItineraryId",
                table: "Memories",
                column: "ItineraryId",
                principalTable: "Itineraries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memories_Itineraries_ItineraryId",
                table: "Memories");

            migrationBuilder.DropIndex(
                name: "IX_Memories_ItineraryId",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "ItineraryId",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Itineraries");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Itineraries");

            migrationBuilder.AddColumn<string>(
                name: "Dates",
                table: "Itineraries",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
