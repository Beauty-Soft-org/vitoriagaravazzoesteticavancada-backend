using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beautysoft.Migrations
{
    /// <inheritdoc />
    public partial class initial2222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rua",
                table: "PerfisUsuarios",
                newName: "Logradouro");

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "PerfisUsuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "PerfisUsuarios");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "PerfisUsuarios",
                newName: "Rua");
        }
    }
}
