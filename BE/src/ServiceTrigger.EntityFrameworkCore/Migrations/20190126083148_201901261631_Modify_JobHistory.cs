using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceTrigger.Migrations
{
    public partial class _201901261631_Modify_JobHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_st_JobHistory_st_job_JobId",
                table: "st_JobHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_st_JobHistory",
                table: "st_JobHistory");

            migrationBuilder.RenameTable(
                name: "st_JobHistory",
                newName: "st_jobhistory");

            migrationBuilder.RenameIndex(
                name: "IX_st_JobHistory_JobId",
                table: "st_jobhistory",
                newName: "IX_st_jobhistory_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_st_jobhistory",
                table: "st_jobhistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_st_jobhistory_st_job_JobId",
                table: "st_jobhistory",
                column: "JobId",
                principalTable: "st_job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_st_jobhistory_st_job_JobId",
                table: "st_jobhistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_st_jobhistory",
                table: "st_jobhistory");

            migrationBuilder.RenameTable(
                name: "st_jobhistory",
                newName: "st_JobHistory");

            migrationBuilder.RenameIndex(
                name: "IX_st_jobhistory_JobId",
                table: "st_JobHistory",
                newName: "IX_st_JobHistory_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_st_JobHistory",
                table: "st_JobHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_st_JobHistory_st_job_JobId",
                table: "st_JobHistory",
                column: "JobId",
                principalTable: "st_job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
