using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class house : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "Voters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "House",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voters_HouseId",
                table: "Voters",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voters_House_HouseId",
                table: "Voters",
                column: "HouseId",
                principalTable: "House",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voters_House_HouseId",
                table: "Voters");

            migrationBuilder.DropTable(
                name: "House");

            migrationBuilder.DropIndex(
                name: "IX_Voters_HouseId",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "Voters");
        }
    }
}
