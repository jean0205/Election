using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.MVC.Migrations
{
    public partial class dead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Dead",
                table: "Voters",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dead",
                table: "Voters");
        }
    }
}
