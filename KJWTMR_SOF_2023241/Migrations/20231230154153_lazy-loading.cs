using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KJWTMR_SOF_2023241.Migrations
{
    public partial class lazyloading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Alcohols",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Alcohols_OwnerId",
                table: "Alcohols",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alcohols_AspNetUsers_OwnerId",
                table: "Alcohols",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alcohols_AspNetUsers_OwnerId",
                table: "Alcohols");

            migrationBuilder.DropIndex(
                name: "IX_Alcohols_OwnerId",
                table: "Alcohols");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Alcohols",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
