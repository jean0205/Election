using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class recordedby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_AspNetUsers_UserId",
                table: "ElectionVotes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ElectionVotes",
                newName: "RecorderById");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionVotes_UserId",
                table: "ElectionVotes",
                newName: "IX_ElectionVotes_RecorderById");

            migrationBuilder.AddColumn<bool>(
                name: "Locked",
                table: "Interviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LockedById",
                table: "Interviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecorderById",
                table: "Interviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Locked",
                table: "ElectionVotes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LockedById",
                table: "ElectionVotes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_LockedById",
                table: "Interviews",
                column: "LockedById");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_RecorderById",
                table: "Interviews",
                column: "RecorderById");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionVotes_LockedById",
                table: "ElectionVotes",
                column: "LockedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_AspNetUsers_LockedById",
                table: "ElectionVotes",
                column: "LockedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_AspNetUsers_RecorderById",
                table: "ElectionVotes",
                column: "RecorderById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_AspNetUsers_LockedById",
                table: "Interviews",
                column: "LockedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_AspNetUsers_RecorderById",
                table: "Interviews",
                column: "RecorderById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_AspNetUsers_LockedById",
                table: "ElectionVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_AspNetUsers_RecorderById",
                table: "ElectionVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_AspNetUsers_LockedById",
                table: "Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_AspNetUsers_RecorderById",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_LockedById",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_RecorderById",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_ElectionVotes_LockedById",
                table: "ElectionVotes");

            migrationBuilder.DropColumn(
                name: "Locked",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "LockedById",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "RecorderById",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "Locked",
                table: "ElectionVotes");

            migrationBuilder.DropColumn(
                name: "LockedById",
                table: "ElectionVotes");

            migrationBuilder.RenameColumn(
                name: "RecorderById",
                table: "ElectionVotes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionVotes_RecorderById",
                table: "ElectionVotes",
                newName: "IX_ElectionVotes_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_AspNetUsers_UserId",
                table: "ElectionVotes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
