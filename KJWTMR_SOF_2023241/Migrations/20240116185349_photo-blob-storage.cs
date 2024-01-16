using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KJWTMR_SOF_2023241.Migrations
{
    public partial class photoblobstorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoData",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "ConetntType",
                table: "Photos",
                newName: "PhotoUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Photos",
                newName: "ConetntType");

            migrationBuilder.AddColumn<byte[]>(
                name: "PhotoData",
                table: "Photos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
