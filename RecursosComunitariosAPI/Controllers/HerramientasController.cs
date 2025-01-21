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
        public async Task<ActionResult<IEnumerable<Herramienta>>> GetHerramientas()
        {
            return await _context.Herramienta.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Herramienta>> GetHerramienta(int id)
        {
            var herramienta = await _context.Herramienta.FindAsync(id);
            if (herramienta == null) return NotFound();
            return herramienta;
        }

        [HttpPost]
        public async Task<ActionResult<Herramienta>> CreateHerramienta(Herramienta herramienta)
        {
            _context.Herramienta.Add(herramienta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHerramienta), new { id = herramienta.ID }, herramienta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHerramienta(int id, Herramienta herramienta)
        {
            if (id != herramienta.ID) return BadRequest();
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
