
using Microsoft.EntityFrameworkCore;
using MundialFeminino.Api.Models;

namespace MundialFeminino.Api.Data.ImportacaoCsv
{
    public class ImportadorArtilheirasCsv
    {
        public async Task<int> ImportarAsync(
            AppDbContext contexto,
            List<LinhaArtilheiraCsv> registros)
        {
            // Impede que o CSV altere qualquer edição histórica.
            if (registros.Any(r => r.Ano != 2027))
            {
                throw new InvalidDataException(
                    "A importação aceita somente registros de 2027.");
            }

            // Impede nomes ou seleções vazios e gols fora do limite.
            if (registros.Any(r =>
                string.IsNullOrWhiteSpace(r.Nome) ||
                r.Nome.Trim().Length < 2 ||
                r.Nome.Trim().Length > 150 ||
                string.IsNullOrWhiteSpace(r.Selecao) ||
                r.Selecao.Trim().Length < 2 ||
                r.Selecao.Trim().Length > 100 ||
                r.Gols < 0 ||
                r.Gols > 100))
            {
                throw new InvalidDataException(
                    "O CSV contém dados inválidos.");
            }

            // Detecta registros repetidos dentro do próprio CSV.
            var duplicados = registros
                .GroupBy(r => new
                {
                    Nome = r.Nome.Trim(),
                    Selecao = r.Selecao.Trim()
                })
                .Any(g => g.Count() > 1);

            if (duplicados)
            {
                throw new InvalidDataException(
                    "O CSV contém jogadoras duplicadas para a mesma seleção.");
            }

            // Consulta somente registros de 2027.
            var existentes = await contexto.GolsJogadoras
                .Where(g => g.Ano == 2027)
                .ToListAsync();

            int alterados = 0;

            foreach (var registro in registros)
            {
                string nome = registro.Nome.Trim();
                string selecao = registro.Selecao.Trim();

                var existente = existentes.FirstOrDefault(g =>
                    g.Nome == nome &&
                    g.Selecao == selecao);

                if (existente != null)
                {
                    // Atualiza o total de gols, sem somar ao anterior.
                    existente.Gols = registro.Gols;
                }
                else
                {
                    // Cadastra uma nova jogadora na edição de 2027.
                    contexto.GolsJogadoras.Add(new GolJogadora
                    {
                        Nome = nome,
                        Selecao = selecao,
                        Ano = 2027,
                        Gols = registro.Gols
                    });
                }

                alterados++;
            }

            // Persiste as alterações no banco.
            await contexto.SaveChangesAsync();

            return alterados;
        }
    }
}

