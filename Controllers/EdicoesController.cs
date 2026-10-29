
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundialFeminino.Api.Data;

namespace MundialFeminino.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EdicoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EdicoesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodas()
        {
            var edicoes = await _context.Edicoes
                .OrderBy(e => e.Ano)
                .ToListAsync();

            return Ok(edicoes);
        }
        [HttpGet("{ano}")]
        public async Task<IActionResult> GetPorAno(int ano)
        {
            var edicao = await _context.Edicoes
                .FirstOrDefaultAsync(e => e.Ano == ano);

            if (edicao == null)
            {
                return NotFound($"Não existe edição para o ano {ano}.");
            }

            return Ok(edicao);
        }
    }
}

