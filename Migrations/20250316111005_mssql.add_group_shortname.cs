using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimetableHelper.Migrations
{
    /// <inheritdoc />
    public partial class mssqladd_group_shortname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Group",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Group");
        }
    }
}
