using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CulemborgWeerAPI.Migrations
{
    public partial class CloudsRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Clouds",
                table: "WeatherInformation",
                newName: "CloudPercentage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CloudPercentage",
                table: "WeatherInformation",
                newName: "Clouds");
        }
    }
}
