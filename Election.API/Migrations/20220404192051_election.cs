using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class election : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectionVote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ElectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    SupportedPartyId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    OtherComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterviewerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectionVote_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ElectionVote_Interviewers_InterviewerId",
                        column: x => x.InterviewerId,
                        principalTable: "Interviewers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ElectionVote_Parties_SupportedPartyId",
                        column: x => x.SupportedPartyId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectionVote_Voters_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Voters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectionVote_CommentId",
                table: "ElectionVote",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionVote_InterviewerId",
                table: "ElectionVote",
                column: "InterviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionVote_SupportedPartyId",
                table: "ElectionVote",
                column: "SupportedPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionVote_VoterId",
                table: "ElectionVote",
                column: "VoterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectionVote");
        }
    }
}
