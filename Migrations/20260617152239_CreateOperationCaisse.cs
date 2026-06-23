using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasytransitCaisse.Migrations
{
    /// <inheritdoc />
    public partial class CreateOperationCaisse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationsCaisses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JourneeCaisseId = table.Column<int>(type: "int", nullable: false),
                    DateOperation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOperation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsCaisses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationsCaisses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperationsCaisses_JourneesCaisses_JourneeCaisseId",
                        column: x => x.JourneeCaisseId,
                        principalTable: "JourneesCaisses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationsCaisses_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationsCaisses_ClientId",
                table: "OperationsCaisses",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsCaisses_JourneeCaisseId",
                table: "OperationsCaisses",
                column: "JourneeCaisseId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsCaisses_UtilisateurId",
                table: "OperationsCaisses",
                column: "UtilisateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationsCaisses");
        }
    }
}
