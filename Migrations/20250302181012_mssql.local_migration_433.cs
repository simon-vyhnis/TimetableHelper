using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimetableHelper.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_433 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Group",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_ClassId",
                table: "Group",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Class_ClassId",
                table: "Group",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Class_ClassId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_ClassId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Group");
        }
    }
}
