using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimetableHelper.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_757 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetHours",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetHours",
                table: "Teacher");
        }
    }
}
