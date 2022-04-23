using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class removingrequired2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_NationalElections_ElectionId",
                table: "ElectionVotes");

            migrationBuilder.AlterColumn<int>(
                name: "ElectionId",
                table: "ElectionVotes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_NationalElections_ElectionId",
                table: "ElectionVotes",
                column: "ElectionId",
                principalTable: "NationalElections",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_NationalElections_ElectionId",
                table: "ElectionVotes");

            migrationBuilder.AlterColumn<int>(
                name: "ElectionId",
                table: "ElectionVotes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_NationalElections_ElectionId",
                table: "ElectionVotes",
                column: "ElectionId",
                principalTable: "NationalElections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
