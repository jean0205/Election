using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class InitialDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Voters_Reg",
                table: "Voters",
                column: "Reg");

            migrationBuilder.CreateIndex(
                name: "IX_PollingDivisions_Name",
                table: "PollingDivisions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_Name",
                table: "Parties",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Constituencies_SGSE",
                table: "Constituencies",
                column: "SGSE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Voters_Reg",
                table: "Voters");

            migrationBuilder.DropIndex(
                name: "IX_PollingDivisions_Name",
                table: "PollingDivisions");

            migrationBuilder.DropIndex(
                name: "IX_Parties_Name",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Constituencies_SGSE",
                table: "Constituencies");
        }
    }
}
