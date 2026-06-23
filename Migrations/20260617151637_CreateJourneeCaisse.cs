using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasytransitCaisse.Migrations
{
    /// <inheritdoc />
    public partial class CreateJourneeCaisse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JourneesCaisses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaisseId = table.Column<int>(type: "int", nullable: false),
                    DateOuverture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFermeture = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoldeInitial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoldeFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourneesCaisses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JourneesCaisses_Caisses_CaisseId",
                        column: x => x.CaisseId,
                        principalTable: "Caisses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JourneesCaisses_CaisseId",
                table: "JourneesCaisses",
                column: "CaisseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JourneesCaisses");
        }
    }
}
