using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class election3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ElectionVotes");

            migrationBuilder.DropColumn(
                name: "ElectionDate",
                table: "ElectionVotes");

            migrationBuilder.AddColumn<int>(
                name: "NationalElectionId",
                table: "Parties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationalElectionId",
                table: "ElectionVotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NationalElections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ElectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalElections", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parties_NationalElectionId",
                table: "Parties",
                column: "NationalElectionId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_NationalElections_NationalElectionId",
                table: "Parties",
                column: "NationalElectionId",
                principalTable: "NationalElections",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_NationalElections_NationalElectionId",
                table: "ElectionVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Parties_NationalElections_NationalElectionId",
                table: "Parties");

            migrationBuilder.DropTable(
                name: "NationalElections");

            migrationBuilder.DropIndex(
                name: "IX_Parties_NationalElectionId",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_ElectionVotes_NationalElectionId",
                table: "ElectionVotes");

            migrationBuilder.DropColumn(
                name: "NationalElectionId",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "NationalElectionId",
                table: "ElectionVotes");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ElectionVotes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ElectionDate",
                table: "ElectionVotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
