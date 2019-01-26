using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceTrigger.Migrations
{
    public partial class _201901261554_Rename_JobHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobHistory_st_job_JobId",
                table: "JobHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobHistory",
                table: "JobHistory");

            migrationBuilder.RenameTable(
                name: "JobHistory",
                newName: "st_JobHistory");

            migrationBuilder.RenameIndex(
                name: "IX_JobHistory_JobId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_st_JobHistory_st_job_JobId",
                table: "st_JobHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_st_JobHistory",
                table: "st_JobHistory");

            migrationBuilder.RenameTable(
                name: "st_JobHistory",
                newName: "JobHistory");

            migrationBuilder.RenameIndex(
                name: "IX_st_JobHistory_JobId",
                table: "JobHistory",
                newName: "IX_JobHistory_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobHistory",
                table: "JobHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobHistory_st_job_JobId",
                table: "JobHistory",
                column: "JobId",
                principalTable: "st_job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
