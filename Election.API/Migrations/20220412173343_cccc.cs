using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class cccc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_NationalElections_NationalElectionId",
                table: "ElectionVotes");

            migrationBuilder.DropIndex(
                name: "IX_ElectionVotes_NationalElectionId",
                table: "ElectionVotes");

            migrationBuilder.DropColumn(
                name: "NationalElectionId",
                table: "ElectionVotes");

            migrationBuilder.AddColumn<int>(
                name: "ElectionId",
                table: "ElectionVotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ElectionVotes_ElectionId",
                table: "ElectionVotes",
                column: "ElectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_NationalElections_ElectionId",
                table: "ElectionVotes",
                column: "ElectionId",
                principalTable: "NationalElections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_NationalElections_ElectionId",
                table: "ElectionVotes");

            migrationBuilder.DropIndex(
                name: "IX_ElectionVotes_ElectionId",
                table: "ElectionVotes");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "ElectionVotes");

            migrationBuilder.AddColumn<int>(
                name: "NationalElectionId",
                table: "ElectionVotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElectionVotes_NationalElectionId",
                table: "ElectionVotes",
                column: "NationalElectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_NationalElections_NationalElectionId",
                table: "ElectionVotes",
                column: "NationalElectionId",
                principalTable: "NationalElections",
                principalColumn: "Id");
        }
    }
}
