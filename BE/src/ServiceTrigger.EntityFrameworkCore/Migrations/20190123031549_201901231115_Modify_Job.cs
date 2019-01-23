using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceTrigger.Migrations
{
    public partial class _201901231115_Modify_Job : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_st_job_ProjectId",
                table: "st_job",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_st_job_st_project_ProjectId",
                table: "st_job",
                column: "ProjectId",
                principalTable: "st_project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_st_job_st_project_ProjectId",
                table: "st_job");

            migrationBuilder.DropIndex(
                name: "IX_st_job_ProjectId",
                table: "st_job");
        }
    }
}
