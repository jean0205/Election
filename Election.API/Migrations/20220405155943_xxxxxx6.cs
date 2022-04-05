using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class xxxxxx6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_NationalElections_NationalElectionId",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_NationalElectionId",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "NationalElectionId",
                table: "Parties");

            migrationBuilder.CreateTable(
                name: "NationalElectionParty",
                columns: table => new
                {
                    NationalElectionsId = table.Column<int>(type: "int", nullable: false),
                    PartiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalElectionParty", x => new { x.NationalElectionsId, x.PartiesId });
                    table.ForeignKey(
                        name: "FK_NationalElectionParty_NationalElections_NationalElectionsId",
                        column: x => x.NationalElectionsId,
                        principalTable: "NationalElections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NationalElectionParty_Parties_PartiesId",
                        column: x => x.PartiesId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NationalElectionParty_PartiesId",
                table: "NationalElectionParty",
                column: "PartiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NationalElectionParty");

            migrationBuilder.AddColumn<int>(
                name: "NationalElectionId",
                table: "Parties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_NationalElectionId",
                table: "Parties",
                column: "NationalElectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_NationalElections_NationalElectionId",
                table: "Parties",
                column: "NationalElectionId",
                principalTable: "NationalElections",
                principalColumn: "Id");
        }
    }
}
