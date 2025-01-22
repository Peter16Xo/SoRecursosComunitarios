using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Models;

namespace APIRecursosComunitarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionHerramientasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservacionHerramientasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReservacionHerramientas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservacionHerramienta>>> GetReservasHerramientas()
        {
            return await _context.ReservasHerramientas.ToListAsync();
        }

        // GET: api/ReservacionHerramientas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservacionHerramienta>> GetReservacionHerramienta(int id)
        {
            var reservacionHerramienta = await _context.ReservasHerramientas.FindAsync(id);

            if (reservacionHerramienta == null)
            {
                return NotFound();
            }

            return reservacionHerramienta;
        }

        // PUT: api/ReservacionHerramientas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservacionHerramienta(int id, ReservacionHerramienta reservacionHerramienta)
        {
            if (id != reservacionHerramienta.ID)
            {
                return BadRequest();
            }

            _context.Entry(reservacionHerramienta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservacionHerramientaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ReservacionHerramientas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReservacionHerramienta>> PostReservacionHerramienta(ReservacionHerramienta reservacionHerramienta)
        {
            _context.ReservasHerramientas.Add(reservacionHerramienta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservacionHerramienta", new { id = reservacionHerramienta.ID }, reservacionHerramienta);
        }

        // DELETE: api/ReservacionHerramientas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservacionHerramienta(int id)
        {
            var reservacionHerramienta = await _context.ReservasHerramientas.FindAsync(id);
            if (reservacionHerramienta == null)
            {
                return NotFound();
            }

            _context.ReservasHerramientas.Remove(reservacionHerramienta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservacionHerramientaExists(int id)
        {
            return _context.ReservasHerramientas.Any(e => e.ID == id);
        }
    }
}
