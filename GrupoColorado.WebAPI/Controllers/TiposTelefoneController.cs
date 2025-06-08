using GrupoColorado.Domain.Entities;
using GrupoColorado.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace GrupoColorado.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiposTelefoneController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TiposTelefoneController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTiposTelefone()
        {
            var tipos = _context.TiposTelefone.ToList();
            return Ok(tipos);
        }
    }
}
