using Microsoft.EntityFrameworkCore.Migrations;

namespace BolindersBil.web.Migrations
{
    public partial class db0003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUpload_Vehicle_VehicleId",
                table: "FileUpload");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Brand_BrandId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Dealership_DealerShipId",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileUpload",
                table: "FileUpload");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dealership",
                table: "Dealership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brand",
                table: "Brand");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameTable(
                name: "FileUpload",
                newName: "FileUploads");

            migrationBuilder.RenameTable(
                name: "Dealership",
                newName: "Dealerships");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "Brands");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_DealerShipId",
                table: "Vehicles",
                newName: "IX_Vehicles_DealerShipId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_BrandId",
                table: "Vehicles",
                newName: "IX_Vehicles_BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_FileUpload_VehicleId",
                table: "FileUploads",
                newName: "IX_FileUploads_VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileUploads",
                table: "FileUploads",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dealerships",
                table: "Dealerships",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploads_Vehicles_VehicleId",
                table: "FileUploads",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Brands_BrandId",
                table: "Vehicles",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Dealerships_DealerShipId",
                table: "Vehicles",
                column: "DealerShipId",
                principalTable: "Dealerships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUploads_Vehicles_VehicleId",
                table: "FileUploads");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Brands_BrandId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Dealerships_DealerShipId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileUploads",
                table: "FileUploads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dealerships",
                table: "Dealerships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameTable(
                name: "FileUploads",
                newName: "FileUpload");

            migrationBuilder.RenameTable(
                name: "Dealerships",
                newName: "Dealership");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "Brand");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_DealerShipId",
                table: "Vehicle",
                newName: "IX_Vehicle_DealerShipId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_BrandId",
                table: "Vehicle",
                newName: "IX_Vehicle_BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_FileUploads_VehicleId",
                table: "FileUpload",
                newName: "IX_FileUpload_VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileUpload",
                table: "FileUpload",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dealership",
                table: "Dealership",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brand",
                table: "Brand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUpload_Vehicle_VehicleId",
                table: "FileUpload",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Brand_BrandId",
                table: "Vehicle",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Dealership_DealerShipId",
                table: "Vehicle",
                column: "DealerShipId",
                principalTable: "Dealership",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
