using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Data.Contexts;

namespace RecursosComunitariosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HerramientasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HerramientasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Herramientas>>> GetHerramientas()
        {
            return await _context.Herramienta.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Herramientas>> GetHerramienta(int id)
        {
            var herramienta = await _context.Herramienta.FindAsync(id);
            if (herramienta == null) return NotFound();
            return herramienta;
        }

        [HttpPost]
        public async Task<ActionResult<Herramientas>> CreateHerramienta(Herramientas herramienta)
        {
            _context.Herramienta.Add(herramienta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHerramienta), new { id = herramienta.Id }, herramienta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHerramienta(int id, Herramientas herramienta)
        {
            if (id != herramienta.Id) return BadRequest();
            _context.Entry(herramienta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHerramienta(int id)
        {
            var herramienta = await _context.Herramienta.FindAsync(id);
            if (herramienta == null) return NotFound();

            _context.Herramienta.Remove(herramienta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
