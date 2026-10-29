
using System.Globalization;

namespace MundialFeminino.Api.Data.ImportacaoCsv
{
    public class LeitorArtilheirasCsv
    {
        public List<LinhaArtilheiraCsv> Ler(string caminhoArquivo)
        {
            if (!File.Exists(caminhoArquivo))
            {
                throw new FileNotFoundException(
                    "O arquivo CSV não foi encontrado.",
                    caminhoArquivo);
            }

            var linhas = File.ReadAllLines(caminhoArquivo);

            if (linhas.Length == 0)
            {
                throw new InvalidDataException(
                    "O arquivo CSV está vazio.");
            }

            string cabecalho = linhas[0]
                .Trim()
                .TrimStart('\uFEFF');

            if (cabecalho != "Nome,Selecao,Ano,Gols")
            {
                throw new InvalidDataException(
                    $"Cabeçalho inválido: '{cabecalho}'. " +
                    "Use: Nome,Selecao,Ano,Gols");
            }

            var resultado = new List<LinhaArtilheiraCsv>();

            for (int i = 1; i < linhas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(linhas[i]))
                    continue;

                var colunas = linhas[i].Split(',');

                if (colunas.Length != 4)
                {
                    throw new InvalidDataException(
                        $"Linha {i + 1}: devem existir exatamente 4 colunas.");
                }

                string nome = colunas[0].Trim();
                string selecao = colunas[1].Trim();

                bool anoValido = int.TryParse(
                    colunas[2].Trim(), out int ano);

                bool golsValidos = int.TryParse(
                    colunas[3].Trim(),
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out int gols);

                if (string.IsNullOrWhiteSpace(nome) ||
                    string.IsNullOrWhiteSpace(selecao))
                {
                    throw new InvalidDataException(
                        $"Linha {i + 1}: nome e seleção são obrigatórios.");
                }

                if (!anoValido || ano != 2027)
                {
                    throw new InvalidDataException(
                        $"Linha {i + 1}: o ano deve ser 2027.");
                }

                if (!golsValidos || gols < 0 || gols > 100)
                {
                    throw new InvalidDataException(
                        $"Linha {i + 1}: os gols devem estar entre 0 e 100.");
                }

                resultado.Add(new LinhaArtilheiraCsv
                {
                    Nome = nome,
                    Selecao = selecao,
                    Ano = ano,
                    Gols = gols
                });
            }

            return resultado;
        }
    }

    public class LinhaArtilheiraCsv
    {
        public string Nome { get; set; } = string.Empty;
        public string Selecao { get; set; } = string.Empty;
        public int Ano { get; set; }
        public int Gols { get; set; }
    }
}
