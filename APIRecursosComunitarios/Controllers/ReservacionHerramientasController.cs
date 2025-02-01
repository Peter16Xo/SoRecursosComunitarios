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
            return await _context.ReservasHerramientas
                .Include(r=>r.Herramienta)
                .Include(r=> r.Usuario)
                .ToListAsync();
        }
        [HttpGet("Herramienta_Reservas")]
        public async Task<ActionResult<IEnumerable<Object>>> GetHerra_Usu_Reservas()
        {
            return await _context.ReservasHerramientas
                .Include(r => r.Herramienta)
                .Include(r => r.Usuario)
                .Select(static r => new
                {
                    r.ID,
                    Usuario = r.Usuario.Nombre + ' ' + r.Usuario.Apellido,
                    Herramienta = r.Herramienta.Nombre,
                    r.Herramienta.Descripcion,
                    r.Dia,
                    r.Fecha,
                    r.HoraInicio,
                    r.HoraFin,
                    r.Disponibilidad,
                })
                .ToListAsync();
        }
        // GET: api/ReservacionHerramientas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservacionHerramienta>> GetReservacionHerramienta(int id)
        {
            var reservacionHerramienta = await _context.ReservasHerramientas
                .Include(r=>r.Herramienta)
                .Include(r=>r.Usuario)
                .FirstOrDefaultAsync();

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
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ReservacionHerramienta>>> SearchReservaHerr(string? nombre, string? apellido, string? instalacion)
        {
            var reservaQuery = _context.ReservasHerramientas
                .Include(r => r.Usuario)
                .Include(r => r.Herramienta)
                .AsQueryable();

            // Filtro por nombre
            if (!string.IsNullOrEmpty(nombre))
            {
                reservaQuery = reservaQuery.Where(r =>
                    r.Usuario.Nombre.Contains(nombre));
            }
            if (!string.IsNullOrEmpty(apellido))
            {
                reservaQuery = reservaQuery.Where(r =>
                r.Usuario.Apellido.Contains(apellido));
            }
            if (!string.IsNullOrEmpty(instalacion))
            {
                reservaQuery = reservaQuery.Where(r =>
                r.Herramienta.Nombre.Contains(instalacion));
            }

            // Proyección de los datos
            var reservas = await reservaQuery
                .Select(r => new
                {
                    r.ID,
                    Usuario = r.Usuario.Nombre + " " + r.Usuario.Apellido,
                    Instalacion = r.Herramienta.Nombre,
                    r.Dia,
                    r.HoraInicio,
                    r.HoraFin,
                    r.Fecha,
                    r.Disponibilidad,
                })
                .ToListAsync();

            // Verificación si no hay resultados
            if (!reservas.Any())
            {
                return NotFound("No se encontraron reservas");
            }

            return Ok(reservas);
        }
        [HttpGet("search_Finalizada")]
        public async Task<ActionResult<IEnumerable<ReservacionHerramienta>>> SearchReservaHerra_Fin(string? nombre, string? apellido, string? instalacion)
        {
            var reservaQuery = _context.ReservasHerramientas
                .Include(r => r.Usuario)
                .Include(r => r.Herramienta)
                .Where(r => r.Disponibilidad == "Finalizada")
                .AsQueryable();

            // Filtro por nombre
            if (!string.IsNullOrEmpty(nombre))
            {
                reservaQuery = reservaQuery.Where(r =>
                    r.Usuario.Nombre.Contains(nombre));
            }
            if (!string.IsNullOrEmpty(apellido))
            {
                reservaQuery = reservaQuery.Where(r =>
                r.Usuario.Apellido.Contains(apellido));
            }
            if (!string.IsNullOrEmpty(instalacion))
            {
                reservaQuery = reservaQuery.Where(r =>
                r.Herramienta.Nombre.Contains(instalacion));
            }

            // Proyección de los datos
            var reservas = await reservaQuery
                .Select(r => new
                {
                    r.ID,
                    Usuario = r.Usuario.Nombre + " " + r.Usuario.Apellido,
                    Instalacion = r.Herramienta.Nombre,
                    r.Dia,
                    r.HoraInicio,
                    r.HoraFin,
                    r.Fecha,
                    r.Disponibilidad,
                })
                .ToListAsync();

            // Verificación si no hay resultados
            if (!reservas.Any())
            {
                return NotFound("No se encontraron reservas");
            }

            return Ok(reservas);
        }
        [HttpGet("Reservas_Herr_Finalizada")]
        public async Task<ActionResult<IEnumerable<Object>>> Get_Reservas_Inst_Fin()
        {
            return await _context.ReservasHerramientas
                .Include(r => r.Herramienta)
                .Include(r => r.Usuario)
                .Where(r => r.Disponibilidad == "Finalizada")
                .Select(r => new
                {
                    r.ID,
                    Usuario = r.Usuario.Nombre + ' ' + r.Usuario.Apellido,
                    Herramienta = r.Herramienta.Nombre,
                    r.Herramienta.Descripcion,
                    r.Dia,
                    r.Fecha,
                    r.HoraInicio,
                    r.HoraFin,
                    r.Disponibilidad,
                }).ToListAsync();
        }
        private bool ReservacionHerramientaExists(int id)
        {
            return _context.ReservasHerramientas.Any(e => e.ID == id);
        }
    }
}
