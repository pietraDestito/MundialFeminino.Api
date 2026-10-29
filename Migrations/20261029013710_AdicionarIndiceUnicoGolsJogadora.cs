using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MundialFeminino.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarIndiceUnicoGolsJogadora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Selecao",
                table: "GolsJogadoras",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "GolsJogadoras",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_GolsJogadoras_Nome_Selecao_Ano",
                table: "GolsJogadoras",
                columns: new[] { "Nome", "Selecao", "Ano" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GolsJogadoras_Nome_Selecao_Ano",
                table: "GolsJogadoras");

            migrationBuilder.AlterColumn<string>(
                name: "Selecao",
                table: "GolsJogadoras",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "GolsJogadoras",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);
        }
    }
}
