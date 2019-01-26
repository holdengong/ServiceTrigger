using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceTrigger.Migrations
{
    public partial class _201901261656_Modify_JobHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_st_jobhistory_st_job_JobId",
                table: "st_jobhistory");

            migrationBuilder.DropIndex(
                name: "IX_st_jobhistory_JobId",
                table: "st_jobhistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_st_jobhistory_JobId",
                table: "st_jobhistory",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_st_jobhistory_st_job_JobId",
                table: "st_jobhistory",
                column: "JobId",
                principalTable: "st_job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
