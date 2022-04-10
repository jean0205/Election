using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class canvas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Canvas_CanvasId",
                table: "Interviews");

            migrationBuilder.AlterColumn<int>(
                name: "CanvasId",
                table: "Interviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Canvas_CanvasId",
                table: "Interviews",
                column: "CanvasId",
                principalTable: "Canvas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Canvas_CanvasId",
                table: "Interviews");

            migrationBuilder.AlterColumn<int>(
                name: "CanvasId",
                table: "Interviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Canvas_CanvasId",
                table: "Interviews",
                column: "CanvasId",
                principalTable: "Canvas",
                principalColumn: "Id");
        }
    }
}
