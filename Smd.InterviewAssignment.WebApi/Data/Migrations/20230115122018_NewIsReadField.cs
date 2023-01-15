using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smd.InterviewAssignment.WebApi.Data.Migrations
{
    public partial class NewIsReadField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Books");
        }
    }
}
