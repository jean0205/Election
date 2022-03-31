using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class active : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voters_House_HouseId",
                table: "Voters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_House",
                table: "House");

            migrationBuilder.RenameTable(
                name: "House",
                newName: "Houses");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Constituencies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Houses",
                table: "Houses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Voters_Houses_HouseId",
                table: "Voters",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voters_Houses_HouseId",
                table: "Voters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Houses",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Constituencies");

            migrationBuilder.RenameTable(
                name: "Houses",
                newName: "House");

            migrationBuilder.AddPrimaryKey(
                name: "PK_House",
                table: "House",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Voters_House_HouseId",
                table: "Voters",
                column: "HouseId",
                principalTable: "House",
                principalColumn: "Id");
        }
    }
}
