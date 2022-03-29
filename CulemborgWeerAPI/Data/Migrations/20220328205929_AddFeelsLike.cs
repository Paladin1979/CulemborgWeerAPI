using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CulemborgWeerAPI.Migrations
{
    public partial class AddFeelsLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TemperatureFeelsLike",
                table: "WeatherInformation",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemperatureFeelsLike",
                table: "WeatherInformation");
        }
    }
}
