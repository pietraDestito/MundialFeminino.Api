using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundialFeminino.Api.Data;
using MundialFeminino.Api.Models;

namespace MundialFeminino.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtilheirasController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public ArtilheirasController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        // GET: api/artilheiras
        [HttpGet]
        public async Task<IActionResult> GetRankingHistorico()
        {
            var ranking = await _contexto.GolsJogadoras
                .GroupBy(j => new { j.Nome, j.Selecao })
                .Select(grupo => new
                {
                    Nome = grupo.Key.Nome,
                    Selecao = grupo.Key.Selecao,
                    TotalGols = grupo.Sum(j => j.Gols)
                })
                .OrderByDescending(j => j.TotalGols)
                .ThenBy(j => j.Nome)
                .Take(20)
                .ToListAsync();

            return Ok(ranking);
        }

        // GET: api/artilheiras/buscar?nome=Marta
        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorNome(
            [FromQuery] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest(
                    "Informe o nome de uma jogadora para pesquisar.");
            }

            var nomePesquisa = nome.Trim();

            var resultado = await _contexto.GolsJogadoras
                .Where(j => j.Nome.Contains(nomePesquisa))
                .GroupBy(j => new { j.Nome, j.Selecao })
                .Select(grupo => new
                {
                    Nome = grupo.Key.Nome,
                    Selecao = grupo.Key.Selecao,
                    TotalGols = grupo.Sum(j => j.Gols)
                })
                .OrderByDescending(j => j.TotalGols)
                .ToListAsync();

            return Ok(resultado);
        }

        // GET: api/artilheiras/edicao/2019
        // Consulta as artilheiras de uma edição válida.
        [HttpGet("edicao/{ano:int}")]
        public async Task<IActionResult> GetArtilheirasPorEdicao(int ano)
        {
            int[] edicoesValidas =
            {
                1991, 1995, 1999, 2003, 2007,
                2011, 2015, 2019, 2023, 2027
            };

            if (!edicoesValidas.Contains(ano))
            {
                return BadRequest("Ano de edição inválido.");
            }

            var artilheiras = await _contexto.GolsJogadoras
                .Where(j => j.Ano == ano)
                .OrderByDescending(j => j.Gols)
                .ThenBy(j => j.Nome)
                .Select(j => new
                {
                    j.Nome,
                    j.Selecao,
                    j.Gols
                })
                .ToListAsync();

            return Ok(artilheiras);
        }

        // PUT: api/artilheiras/2027
        // Permite cadastrar ou atualizar dados somente de 2027.
        [HttpPut("2027")]
        public async Task<IActionResult> AtualizarGols2027(
            [FromBody] AtualizarGolsRequest requisicao)
        {
            if (requisicao.Ano != 2027)
            {
                return BadRequest(
                    "Este endpoint permite atualizar apenas os dados de 2027.");
            }

            var nome = requisicao.Nome.Trim();
            var selecao = requisicao.Selecao.Trim();

            var jogadora = await _contexto.GolsJogadoras
                .FirstOrDefaultAsync(j =>
                    j.Nome == nome &&
                    j.Selecao == selecao &&
                    j.Ano == 2027);

            bool foiCriada = jogadora == null;

            if (foiCriada)
            {
                jogadora = new GolJogadora
                {
                    Nome = nome,
                    Selecao = selecao,
                    Ano = 2027,
                    Gols = requisicao.Gols
                };

                _contexto.GolsJogadoras.Add(jogadora);
            }
            else
            {
                jogadora!.Gols = requisicao.Gols;
            }

            await _contexto.SaveChangesAsync();

            var resposta = new
            {
                mensagem = foiCriada
                    ? "Jogadora cadastrada com sucesso."
                    : "Dados da jogadora atualizados com sucesso.",
                jogadora!.Nome,
                jogadora.Selecao,
                jogadora.Ano,
                jogadora.Gols
            };

            if (foiCriada)
            {
                return StatusCode(
                    StatusCodes.Status201Created,
                    resposta);
            }

            return Ok(resposta);
        }
    }
}
