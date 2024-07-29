using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCWeb.NET.Migrations
{
    /// <inheritdoc />
    public partial class AddPostDeletedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Posts",
                type: "timestamp with time zone",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldRowVersion: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Posts",
                type: "timestamp with time zone",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Posts",
                type: "timestamp with time zone",
                rowVersion: true,
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldRowVersion: true,
                oldNullable: true);
        }
    }
}
