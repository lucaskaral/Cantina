using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CantinaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Cantina2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amout",
                table: "OrderProducts",
                newName: "Amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "OrderProducts",
                newName: "Amout");
        }
    }
}
