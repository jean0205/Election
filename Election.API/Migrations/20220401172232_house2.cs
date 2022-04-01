using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Election.API.Migrations
{
    public partial class house2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Voters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Interviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Interviewers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Houses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Houses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Houses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPersons",
                table: "Houses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "CanvasTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Canvas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Interviewers");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "NumberOfPersons",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "CanvasTypes");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Canvas");
        }
    }
}
