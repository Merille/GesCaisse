using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasytransitCaisse.Migrations
{
    /// <inheritdoc />
    public partial class AddNumeroJournee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroJournee",
                table: "JourneesCaisses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroJournee",
                table: "JourneesCaisses");
        }
    }
}
