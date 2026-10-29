
using System.Text.RegularExpressions;
using MundialFeminino.Api.Models;

namespace MundialFeminino.Api.Data
{
    public static class LeitorArtilheiras
    {
        public static List<GolJogadora> LerArquivo(string caminho)
        {
            string html = File.ReadAllText(caminho);

            var bloco = Regex.Match(
                html,
                @"<pre[^>]*>(.*?)</pre>",
                RegexOptions.Singleline |
                RegexOptions.IgnoreCase
            );

            if (!bloco.Success)
            {
                throw new InvalidOperationException(
                    "Não foi encontrado o bloco de dados no HTML."
                );
            }

            var jogadoras = new List<GolJogadora>();

            var linhas = bloco.Groups[1].Value.Split(
                new[] { "\r\n", "\n" },
                StringSplitOptions.RemoveEmptyEntries
            );

            var regexJogadora = new Regex(
                @"^\s*(?<nome>.+?)\s+\((?<selecao>[^()]+)\)\s+\d+\s+\((?<distribuicao>[^()]*)\)\s*$"
            );

            var regexGols = new Regex(
                @"(?<ano>19\d{2}|20\d{2})\s*,?\s*(?<gols>\d+)"
            );

            foreach (var linha in linhas)
            {
                var resultado = regexJogadora.Match(linha);

                if (!resultado.Success)
                {
                    continue;
                }

                string nome = resultado.Groups["nome"].Value.Trim();
                string selecao = resultado.Groups["selecao"].Value.Trim();
                string distribuicao = resultado.Groups["distribuicao"].Value;

                var registros = regexGols.Matches(distribuicao);

                foreach (Match registro in registros)
                {
                    int ano = int.Parse(registro.Groups["ano"].Value);
                    int gols = int.Parse(registro.Groups["gols"].Value);

                    if (ano < 1991 || ano > 2023 || gols <= 0)
                    {
                        continue;
                    }

                    jogadoras.Add(new GolJogadora
                    {
                        Nome = nome,
                        Selecao = selecao,
                        Ano = ano,
                        Gols = gols
                    });
                }
            }

            return jogadoras;
        }
    }
}

