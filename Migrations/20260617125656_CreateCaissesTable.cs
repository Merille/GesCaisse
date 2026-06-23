using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasytransitCaisse.Migrations
{
    /// <inheritdoc />
    public partial class CreateCaissesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caisses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChkCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChkDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesPersonDefault = table.Column<int>(type: "int", nullable: true),
                    CashierDefault = table.Column<int>(type: "int", nullable: true),
                    CustomerDefaultCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JournalDefaultCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caisses", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caisses");
        }
    }
}
