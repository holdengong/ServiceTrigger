using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceTrigger.Migrations
{
    public partial class _201901261540_Modify_JobHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "JobHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobHistory_JobId",
                table: "JobHistory",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobHistory_st_job_JobId",
                table: "JobHistory",
                column: "JobId",
                principalTable: "st_job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobHistory_st_job_JobId",
                table: "JobHistory");

            migrationBuilder.DropIndex(
                name: "IX_JobHistory_JobId",
                table: "JobHistory");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobHistory");
        }
    }
}
