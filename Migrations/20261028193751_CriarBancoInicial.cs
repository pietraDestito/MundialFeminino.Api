using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MundialFeminino.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriarBancoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Edicoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    PaisSede = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Campeao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViceCampeao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edicoes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Edicoes");
        }
    }
}
