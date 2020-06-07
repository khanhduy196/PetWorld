using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User.Migrations
{
    public partial class Updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Pets");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Pets",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Pets");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Pets",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");
        }
    }
}
