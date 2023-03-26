using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meridian_Web.Migrations
{
    public partial class ChangeSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "BasketProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "BasketProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketProducts_ColorId",
                table: "BasketProducts",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketProducts_SizeId",
                table: "BasketProducts",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Colors_ColorId",
                table: "BasketProducts",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Sizes_SizeId",
                table: "BasketProducts",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Colors_ColorId",
                table: "BasketProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Sizes_SizeId",
                table: "BasketProducts");

            migrationBuilder.DropIndex(
                name: "IX_BasketProducts_ColorId",
                table: "BasketProducts");

            migrationBuilder.DropIndex(
                name: "IX_BasketProducts_SizeId",
                table: "BasketProducts");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "BasketProducts");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "BasketProducts");
        }
    }
}
