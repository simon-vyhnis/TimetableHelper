using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimetableHelper.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_654 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Student_StudentId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_StudentId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Group");

            migrationBuilder.CreateTable(
                name: "GroupStudent",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStudent", x => new { x.GroupsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_GroupStudent_Group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupStudent_Student_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupStudent_StudentsId",
                table: "GroupStudent",
                column: "StudentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupStudent");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Group",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_StudentId",
                table: "Group",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Student_StudentId",
                table: "Group",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");
        }
    }
}
