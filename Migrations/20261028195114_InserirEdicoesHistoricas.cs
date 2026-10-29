using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MundialFeminino.Api.Migrations
{
    /// <inheritdoc />
    public partial class InserirEdicoesHistoricas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Edicoes",
                columns: new[] { "Id", "Ano", "Campeao", "PaisSede", "ViceCampeao" },
                values: new object[,]
                {
                    { 1, 1991, "Estados Unidos", "China", "Noruega" },
                    { 2, 1995, "Noruega", "Suécia", "Alemanha" },
                    { 3, 1999, "Estados Unidos", "Estados Unidos", "China" },
                    { 4, 2003, "Alemanha", "Estados Unidos", "Suécia" },
                    { 5, 2007, "Alemanha", "China", "Brasil" },
                    { 6, 2011, "Japão", "Alemanha", "Estados Unidos" },
                    { 7, 2015, "Estados Unidos", "Canadá", "Japão" },
                    { 8, 2019, "Estados Unidos", "França", "Países Baixos" },
                    { 9, 2023, "Espanha", "Austrália e Nova Zelândia", "Inglaterra" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Edicoes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Edicoes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Edicoes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Edicoes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Edicoes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Edicoes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Edicoes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Edicoes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Edicoes",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
