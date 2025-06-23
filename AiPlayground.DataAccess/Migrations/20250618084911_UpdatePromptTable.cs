using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiPlayground.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePromptTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SystemMsg",
                table: "Prompt",
                newName: "SystemMessage");

            migrationBuilder.RenameColumn(
                name: "ExpectedResponse",
                table: "Prompt",
                newName: "ExpectedResult");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SystemMessage",
                table: "Prompt",
                newName: "SystemMsg");

            migrationBuilder.RenameColumn(
                name: "ExpectedResult",
                table: "Prompt",
                newName: "ExpectedResponse");
        }
    }
}
