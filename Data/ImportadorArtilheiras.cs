
using Microsoft.EntityFrameworkCore;
using MundialFeminino.Api.Models;

namespace MundialFeminino.Api.Data
{
    public static class ImportadorArtilheiras
    {
        public static void Validar(List<GolJogadora> registros)
        {
            if (registros.Count == 0)
            {
                throw new InvalidOperationException(
                    "Nenhum registro foi encontrado no arquivo.");
            }

            var invalidos = registros
                .Where(j =>
                    string.IsNullOrWhiteSpace(j.Nome) ||
                    string.IsNullOrWhiteSpace(j.Selecao) ||
                    j.Ano < 1991 ||
                    j.Ano > 2023 ||
                    j.Gols <= 0)
                .ToList();

            if (invalidos.Count > 0)
            {
                throw new InvalidOperationException(
                    $"Foram encontrados {invalidos.Count} registros inválidos.");
            }

            var duplicados = registros
                .GroupBy(j => new
                {
                    Nome = j.Nome.Trim(),
                    Selecao = j.Selecao.Trim(),
                    j.Ano
                })
                .Where(grupo => grupo.Count() > 1)
                .ToList();

            if (duplicados.Count > 0)
            {
                throw new InvalidOperationException(
                    $"Foram encontrados {duplicados.Count} grupos duplicados.");
            }

            Console.WriteLine("Validação básica concluída.");
            Console.WriteLine($"Registros válidos: {registros.Count}");
            Console.WriteLine(
                $"Jogadoras diferentes: {registros.Select(j => j.Nome).Distinct().Count()}");
        }

        public static async Task ImportarHistoricoAsync(
            AppDbContext contexto,
            List<GolJogadora> registros)
        {
            Validar(registros);

            // Protege os dados caso a tabela já tenha registros.
            bool tabelaPossuiDados =
                await contexto.GolsJogadoras.AnyAsync();

            if (tabelaPossuiDados)
            {
                throw new InvalidOperationException(
                    "A tabela já possui dados. A importação foi cancelada para evitar duplicações.");
            }

            await using var transacao =
                await contexto.Database.BeginTransactionAsync();

            try
            {
                await contexto.GolsJogadoras.AddRangeAsync(registros);

                await contexto.SaveChangesAsync();

                await transacao.CommitAsync();

                Console.WriteLine(
                    $"Importação concluída: {registros.Count} registros salvos.");
            }
            catch
            {
                await transacao.RollbackAsync();
                throw;
            }
        }
    }
}

