using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class xxxxxx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_Parties_SupportedPartyId",
                table: "ElectionVotes");

            migrationBuilder.AlterColumn<int>(
                name: "SupportedPartyId",
                table: "ElectionVotes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Voted",
                table: "ElectionVotes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_Parties_SupportedPartyId",
                table: "ElectionVotes",
                column: "SupportedPartyId",
                principalTable: "Parties",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_Parties_SupportedPartyId",
                table: "ElectionVotes");

            migrationBuilder.DropColumn(
                name: "Voted",
                table: "ElectionVotes");

            migrationBuilder.AlterColumn<int>(
                name: "SupportedPartyId",
                table: "ElectionVotes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_Parties_SupportedPartyId",
                table: "ElectionVotes",
                column: "SupportedPartyId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
