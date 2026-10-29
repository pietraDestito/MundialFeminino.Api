
namespace MundialFeminino.Api.Models
{
    public class GolJogadora
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Selecao { get; set; } = string.Empty;

        public int Ano { get; set; }

        public int Gols { get; set; }
    }
}

