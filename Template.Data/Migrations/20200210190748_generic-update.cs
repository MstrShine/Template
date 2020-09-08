using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.DAL.Migrations
{
    public partial class genericupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Users",
                nullable: true);

            migrationBuilder.Sql($"UPDATE Users SET CreatedOn=GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Users",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Users");
        }
    }
}
