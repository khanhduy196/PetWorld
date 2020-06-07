using Microsoft.EntityFrameworkCore.Migrations;

namespace User.Migrations
{
    public partial class UpdatePetTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Pets",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Pets",
                type: "bit",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(bool));
        }
    }
}
