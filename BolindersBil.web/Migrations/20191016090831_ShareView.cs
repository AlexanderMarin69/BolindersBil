using Microsoft.EntityFrameworkCore.Migrations;

namespace BolindersBil.web.Migrations
{
    public partial class ShareView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Vehicles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Vehicles",
                nullable: true);
        }
    }
}
