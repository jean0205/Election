using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class removingrequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_Voters_VoterId",
                table: "ElectionVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Canvas_CanvasId",
                table: "Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Parties_SupportedPartyId",
                table: "Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Voters_VoterId",
                table: "Interviews");

            migrationBuilder.AlterColumn<int>(
                name: "VoterId",
                table: "Interviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SupportedPartyId",
                table: "Interviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CanvasId",
                table: "Interviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "VoterId",
                table: "ElectionVotes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryKey",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_Voters_VoterId",
                table: "ElectionVotes",
                column: "VoterId",
                principalTable: "Voters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Canvas_CanvasId",
                table: "Interviews",
                column: "CanvasId",
                principalTable: "Canvas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Parties_SupportedPartyId",
                table: "Interviews",
                column: "SupportedPartyId",
                principalTable: "Parties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Voters_VoterId",
                table: "Interviews",
                column: "VoterId",
                principalTable: "Voters",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_Voters_VoterId",
                table: "ElectionVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Canvas_CanvasId",
                table: "Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Parties_SupportedPartyId",
                table: "Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Voters_VoterId",
                table: "Interviews");

            migrationBuilder.AlterColumn<int>(
                name: "VoterId",
                table: "Interviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SupportedPartyId",
                table: "Interviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CanvasId",
                table: "Interviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VoterId",
                table: "ElectionVotes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryKey",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_Voters_VoterId",
                table: "ElectionVotes",
                column: "VoterId",
                principalTable: "Voters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Canvas_CanvasId",
                table: "Interviews",
                column: "CanvasId",
                principalTable: "Canvas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Parties_SupportedPartyId",
                table: "Interviews",
                column: "SupportedPartyId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Voters_VoterId",
                table: "Interviews",
                column: "VoterId",
                principalTable: "Voters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
