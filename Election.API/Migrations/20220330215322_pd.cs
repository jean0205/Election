using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class pd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollingDivisions_Voters_VoterId",
                table: "PollingDivisions");

            migrationBuilder.DropIndex(
                name: "IX_PollingDivisions_VoterId",
                table: "PollingDivisions");

            migrationBuilder.DropColumn(
                name: "VoterId",
                table: "PollingDivisions");

            migrationBuilder.AddColumn<int>(
                name: "PollingDivisionId",
                table: "Voters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voters_PollingDivisionId",
                table: "Voters",
                column: "PollingDivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voters_PollingDivisions_PollingDivisionId",
                table: "Voters",
                column: "PollingDivisionId",
                principalTable: "PollingDivisions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voters_PollingDivisions_PollingDivisionId",
                table: "Voters");

            migrationBuilder.DropIndex(
                name: "IX_Voters_PollingDivisionId",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "PollingDivisionId",
                table: "Voters");

            migrationBuilder.AddColumn<int>(
                name: "VoterId",
                table: "PollingDivisions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PollingDivisions_VoterId",
                table: "PollingDivisions",
                column: "VoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PollingDivisions_Voters_VoterId",
                table: "PollingDivisions",
                column: "VoterId",
                principalTable: "Voters",
                principalColumn: "Id");
        }
    }
}
