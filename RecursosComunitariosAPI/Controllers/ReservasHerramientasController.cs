using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Data.Contexts;

namespace RecursosComunitariosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasHerramientasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservasHerramientasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservacionHerramientas>>> GetReservasHerramientas()
        {
            return await _context.ReservasHerramientas
                .Include(r => r.UsuarioId) // Si estas propiedades son navegación, ajusta aquí también
                .Include(r => r.HerramientaId)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservacionHerramientas>> GetReservaHerramienta(int id)
        {
            var reserva = await _context.ReservasHerramientas
                .Include(r => r.UsuarioId)
                .Include(r => r.HerramientaId)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null) return NotFound();
            return reserva;
        }

        [HttpPost]
        public async Task<ActionResult<ReservacionHerramientas>> CreateReservaHerramienta(ReservacionHerramientas reserva)
        {
            _context.ReservasHerramientas.Add(reserva);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReservaHerramienta), new { id = reserva.Id }, reserva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservaHerramienta(int id, ReservacionHerramientas reserva)
        {
            if (id != reserva.Id) return BadRequest();
            _context.Entry(reserva).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservaHerramienta(int id)
        {
            var reserva = await _context.ReservasHerramientas.FindAsync(id);
            if (reserva == null) return NotFound();

            _context.ReservasHerramientas.Remove(reserva);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}