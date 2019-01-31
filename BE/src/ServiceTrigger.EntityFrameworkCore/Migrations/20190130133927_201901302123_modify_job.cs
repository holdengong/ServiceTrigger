using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceTrigger.Migrations
{
    public partial class _201901302123_modify_job : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cron",
                table: "st_job",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cron",
                table: "st_job");
        }
    }
}
