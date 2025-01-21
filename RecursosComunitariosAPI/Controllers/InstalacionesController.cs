using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Data.Contexts;

namespace RecursosComunitariosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstalacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InstalacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instalacion>>> GetInstalaciones()
        {
            return await _context.Instalacion.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Instalacion>> GetInstalacion(int id)
        {
            var instalacion = await _context.Instalacion.FindAsync(id);
            if (instalacion == null) return NotFound();
            return instalacion;
        }

        [HttpPost]
        public async Task<ActionResult<Instalacion>> CreateInstalacion(Instalacion instalacion)
        {
            _context.Instalacion.Add(instalacion);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetInstalacion), new { id = instalacion.Id }, instalacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstalacion(int id, Instalacion instalacion)
        {
            if (id != instalacion.Id) return BadRequest();
            _context.Entry(instalacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstalacion(int id)
        {
            var instalacion = await _context.Instalacion.FindAsync(id);
            if (instalacion == null) return NotFound();

            _context.Instalacion.Remove(instalacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
