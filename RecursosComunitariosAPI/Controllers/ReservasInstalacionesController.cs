using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Data.Contexts;

namespace RecursosComunitariosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasInstalacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservasInstalacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservacionInstalaciones>>> GetReservasInstalaciones()
        {
            return await _context.ReservasInstalaciones
                .Include(r => r.UsuarioId)
                .Include(r => r.InstalacionId)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservacionInstalaciones>> GetReservaInstalacion(int id)
        {
            var reserva = await _context.ReservasInstalaciones
                .Include(r => r.UsuarioId)
                .Include(r => r.InstalacionId)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null) return NotFound();
            return reserva;
        }

        [HttpPost]
        public async Task<ActionResult<ReservacionInstalaciones>> CreateReservaInstalacion(ReservacionInstalaciones reserva)
        {
            _context.ReservasInstalaciones.Add(reserva);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReservaInstalacion), new { id = reserva.Id }, reserva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservaInstalacion(int id, ReservacionInstalaciones reserva)
        {
            if (id != reserva.Id) return BadRequest();
            _context.Entry(reserva).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservaInstalacion(int id)
        {
            var reserva = await _context.ReservasInstalaciones.FindAsync(id);
            if (reserva == null) return NotFound();

            _context.ReservasInstalaciones.Remove(reserva);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}