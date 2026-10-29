
using System.ComponentModel.DataAnnotations;

namespace MundialFeminino.Api.Models
{
    public class AtualizarGolsRequest
    {
        [Required(ErrorMessage = "O nome da jogadora é obrigatório.")]
        [StringLength(150, MinimumLength = 2,
            ErrorMessage = "O nome deve ter entre 2 e 150 caracteres.")]
        [RegularExpression(@".*\S.*",
            ErrorMessage = "O nome não pode conter apenas espaços.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A seleção é obrigatória.")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "A seleção deve ter entre 2 e 100 caracteres.")]
        [RegularExpression(@".*\S.*",
            ErrorMessage = "A seleção não pode conter apenas espaços.")]
        public string Selecao { get; set; } = string.Empty;

        [Range(1991, 2027,
            ErrorMessage = "O ano deve estar entre 1991 e 2027.")]
        public int Ano { get; set; }

        [Range(0, 100,
            ErrorMessage = "A quantidade de gols deve estar entre 0 e 100.")]
        public int Gols { get; set; }
    }
}

