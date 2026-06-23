using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasytransitCaisse.Migrations
{
    /// <inheritdoc />
    public partial class AddCaisseIdToOperationCaisse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaisseId",
                table: "OperationsCaisses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OperationsCaisses_CaisseId",
                table: "OperationsCaisses",
                column: "CaisseId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsCaisses_Caisses_CaisseId",
                table: "OperationsCaisses",
                column: "CaisseId",
                principalTable: "Caisses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationsCaisses_Caisses_CaisseId",
                table: "OperationsCaisses");

            migrationBuilder.DropIndex(
                name: "IX_OperationsCaisses_CaisseId",
                table: "OperationsCaisses");

            migrationBuilder.DropColumn(
                name: "CaisseId",
                table: "OperationsCaisses");
        }
    }
}
