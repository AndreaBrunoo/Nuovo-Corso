using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rubrica.Api.Migrations
{
    /// <inheritdoc />
    public partial class ritorno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Eta",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eta",
                table: "AspNetUsers");
        }
    }
}
