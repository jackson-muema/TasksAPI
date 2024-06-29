using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "TaskItems",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Secret",
                table: "TaskItems");
        }
    }
}
