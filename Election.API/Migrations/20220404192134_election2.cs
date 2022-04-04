using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class election2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVote_Comments_CommentId",
                table: "ElectionVote");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVote_Interviewers_InterviewerId",
                table: "ElectionVote");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVote_Parties_SupportedPartyId",
                table: "ElectionVote");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVote_Voters_VoterId",
                table: "ElectionVote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectionVote",
                table: "ElectionVote");

            migrationBuilder.RenameTable(
                name: "ElectionVote",
                newName: "ElectionVotes");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionVote_VoterId",
                table: "ElectionVotes",
                newName: "IX_ElectionVotes_VoterId");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionVote_SupportedPartyId",
                table: "ElectionVotes",
                newName: "IX_ElectionVotes_SupportedPartyId");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionVote_InterviewerId",
                table: "ElectionVotes",
                newName: "IX_ElectionVotes_InterviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionVote_CommentId",
                table: "ElectionVotes",
                newName: "IX_ElectionVotes_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectionVotes",
                table: "ElectionVotes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_Comments_CommentId",
                table: "ElectionVotes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_Interviewers_InterviewerId",
                table: "ElectionVotes",
                column: "InterviewerId",
                principalTable: "Interviewers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_Parties_SupportedPartyId",
                table: "ElectionVotes",
                column: "SupportedPartyId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVotes_Voters_VoterId",
                table: "ElectionVotes",
                column: "VoterId",
                principalTable: "Voters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_Comments_CommentId",
                table: "ElectionVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_Interviewers_InterviewerId",
                table: "ElectionVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_Parties_SupportedPartyId",
                table: "ElectionVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVotes_Voters_VoterId",
                table: "ElectionVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectionVotes",
                table: "ElectionVotes");

            migrationBuilder.RenameTable(
                name: "ElectionVotes",
                newName: "ElectionVote");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionVotes_VoterId",
                table: "ElectionVote",
                newName: "IX_ElectionVote_VoterId");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionVotes_SupportedPartyId",
                table: "ElectionVote",
                newName: "IX_ElectionVote_SupportedPartyId");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionVotes_InterviewerId",
                table: "ElectionVote",
                newName: "IX_ElectionVote_InterviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionVotes_CommentId",
                table: "ElectionVote",
                newName: "IX_ElectionVote_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectionVote",
                table: "ElectionVote",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVote_Comments_CommentId",
                table: "ElectionVote",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVote_Interviewers_InterviewerId",
                table: "ElectionVote",
                column: "InterviewerId",
                principalTable: "Interviewers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVote_Parties_SupportedPartyId",
                table: "ElectionVote",
                column: "SupportedPartyId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVote_Voters_VoterId",
                table: "ElectionVote",
                column: "VoterId",
                principalTable: "Voters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
