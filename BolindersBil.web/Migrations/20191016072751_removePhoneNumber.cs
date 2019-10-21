using Microsoft.EntityFrameworkCore.Migrations;

namespace BolindersBil.web.Migrations
{
    public partial class removePhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Vehicles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Vehicles",
                nullable: true);
        }
    }
}
