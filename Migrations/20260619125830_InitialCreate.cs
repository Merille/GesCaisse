using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasytransitCaisse.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Clients_ClientId",
                table: "Factures");

            migrationBuilder.DropForeignKey(
                name: "FK_JourneesCaisses_Caisses_CaisseId",
                table: "JourneesCaisses");

            migrationBuilder.DropForeignKey(
                name: "FK_LignesFactures_Factures_FactureId",
                table: "LignesFactures");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsCaisses_Caisses_CaisseId",
                table: "OperationsCaisses");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsCaisses_Clients_ClientId",
                table: "OperationsCaisses");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsCaisses_JourneesCaisses_JourneeCaisseId",
                table: "OperationsCaisses");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsCaisses_Utilisateurs_UtilisateurId",
                table: "OperationsCaisses");

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Clients_ClientId",
                table: "Factures",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JourneesCaisses_Caisses_CaisseId",
                table: "JourneesCaisses",
                column: "CaisseId",
                principalTable: "Caisses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LignesFactures_Factures_FactureId",
                table: "LignesFactures",
                column: "FactureId",
                principalTable: "Factures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsCaisses_Caisses_CaisseId",
                table: "OperationsCaisses",
                column: "CaisseId",
                principalTable: "Caisses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsCaisses_Clients_ClientId",
                table: "OperationsCaisses",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsCaisses_JourneesCaisses_JourneeCaisseId",
                table: "OperationsCaisses",
                column: "JourneeCaisseId",
                principalTable: "JourneesCaisses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsCaisses_Utilisateurs_UtilisateurId",
                table: "OperationsCaisses",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Clients_ClientId",
                table: "Factures");

            migrationBuilder.DropForeignKey(
                name: "FK_JourneesCaisses_Caisses_CaisseId",
                table: "JourneesCaisses");

            migrationBuilder.DropForeignKey(
                name: "FK_LignesFactures_Factures_FactureId",
                table: "LignesFactures");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsCaisses_Caisses_CaisseId",
                table: "OperationsCaisses");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsCaisses_Clients_ClientId",
                table: "OperationsCaisses");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsCaisses_JourneesCaisses_JourneeCaisseId",
                table: "OperationsCaisses");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsCaisses_Utilisateurs_UtilisateurId",
                table: "OperationsCaisses");

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Clients_ClientId",
                table: "Factures",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JourneesCaisses_Caisses_CaisseId",
                table: "JourneesCaisses",
                column: "CaisseId",
                principalTable: "Caisses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LignesFactures_Factures_FactureId",
                table: "LignesFactures",
                column: "FactureId",
                principalTable: "Factures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsCaisses_Caisses_CaisseId",
                table: "OperationsCaisses",
                column: "CaisseId",
                principalTable: "Caisses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsCaisses_Clients_ClientId",
                table: "OperationsCaisses",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsCaisses_JourneesCaisses_JourneeCaisseId",
                table: "OperationsCaisses",
                column: "JourneeCaisseId",
                principalTable: "JourneesCaisses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsCaisses_Utilisateurs_UtilisateurId",
                table: "OperationsCaisses",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
