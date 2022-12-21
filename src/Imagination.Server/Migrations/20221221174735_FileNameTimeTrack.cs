using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imagination.Migrations
{
    /// <inheritdoc />
    public partial class FileNameTimeTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UploadDate",
                table: "UploadEntity",
                newName: "UploadEndDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadBeginDate",
                table: "UploadEntity",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadBeginDate",
                table: "UploadEntity");

            migrationBuilder.RenameColumn(
                name: "UploadEndDate",
                table: "UploadEntity",
                newName: "UploadDate");
        }
    }
}
