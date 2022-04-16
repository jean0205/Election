using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class dobnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "Voters",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ElectionVotes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElectionVotes_UserId",
                table: "ElectionVotes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_AspNetUsers_UserId",
                table: "ElectionVotes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_AspNetUsers_UserId",
                table: "ElectionVotes");

            migrationBuilder.DropIndex(
                name: "IX_ElectionVotes_UserId",
                table: "ElectionVotes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ElectionVotes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "Voters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
