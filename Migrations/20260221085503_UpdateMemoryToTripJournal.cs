using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelItineraryApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMemoryToTripJournal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Liked",
                table: "Memories");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Memories",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "UserProfileId",
                table: "Memories",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "Memories",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Memories",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "Memories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Highlight",
                table: "Memories",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Memories",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Mood",
                table: "Memories",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Memories",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Memories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caption",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Highlight",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Mood",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Memories");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Memories",
                newName: "Image");

            migrationBuilder.UpdateData(
                table: "Memories",
                keyColumn: "UserProfileId",
                keyValue: null,
                column: "UserProfileId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserProfileId",
                table: "Memories",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Memories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Memories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Liked",
                table: "Memories",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
