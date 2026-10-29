
namespace MundialFeminino.Api.Models
{
    public class Edicao
    {
        public int Id { get; set; }

        public int Ano { get; set; }

        public string PaisSede { get; set; } = string.Empty;

        public string Campeao { get; set; } = string.Empty;

        public string ViceCampeao { get; set; } = string.Empty;
    }
}

