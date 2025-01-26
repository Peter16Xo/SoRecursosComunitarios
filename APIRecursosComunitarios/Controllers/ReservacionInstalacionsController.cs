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
    public class ReservacionInstalacionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservacionInstalacionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReservacionInstalacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservacionInstalacion>>> GetReservasInstalaciones()
        {
            //return await _context.ReservasInstalaciones.ToListAsync();
            return await _context.ReservasInstalaciones
                .Include(r => r.Instalacion)
                .Include(r => r.Usuario)
                .ToListAsync();
        }
        [HttpGet("Instalacion_Reservas")]
        public async Task<ActionResult<IEnumerable<Object>>> GetInstal_Usu_Reservas()
        {
            return await _context.ReservasInstalaciones
                .Include(r => r.Instalacion)
                .Include(r => r.Usuario)
                .Select(static r => new
                {
                    r.ID,
                    Usuario = r.Usuario.Nombre + ' ' + r.Usuario.Apellido,
                    Instalacion = r.Instalacion.Nombre,
                    r.Instalacion.Dia,
                    r.Instalacion.HoraInicio,
                    r.Instalacion.HoraFin,
                    r.Fecha,
                    r.Disponibilidad,
                })
                .ToListAsync();
        }
        // GET: api/ReservacionInstalacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservacionInstalacion>> GetReservacionInstalacion(int id)
        {
            var reservacionInstalacion = await _context.ReservasInstalaciones
                .Include(r => r.Instalacion) // Cargar la propiedad Instalacion
                .Include(r => r.Usuario)     // Cargar la propiedad Usuario
                .FirstOrDefaultAsync(r => r.ID == id); // Buscar la entidad por id

            if (reservacionInstalacion == null)
            {
                return NotFound();
            }

            return reservacionInstalacion;
        }
        [HttpGet("Reservas_Inst_Finalizada")]
        public async Task<ActionResult<IEnumerable<Object>>> Get_Reservas_Inst_Fin()
        {
            return await _context.ReservasInstalaciones
                .Include(r => r.Instalacion)
                .Include(r => r.Usuario)
                .Where(r => r.Disponibilidad == "Finalizada")
                .Select(r => new
                {
                    r.ID,
                    Usuario = r.Usuario.Nombre + ' ' + r.Usuario.Apellido,
                    Instalacion = r.Instalacion.Nombre,
                    r.Instalacion.Dia,
                    r.Instalacion.HoraInicio,
                    r.Instalacion.HoraFin,
                    r.Fecha,
                    r.Disponibilidad,
                }).ToListAsync();
        }
        // PUT: api/ReservacionInstalacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservacionInstalacion(int id, ReservacionInstalacion reservacionInstalacion)
        {
            if (id != reservacionInstalacion.ID)
            {
                return BadRequest();
            }

            _context.Entry(reservacionInstalacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservacionInstalacionExists(id))
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
        
        // POST: api/ReservacionInstalacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReservacionInstalacion>> PostReservacionInstalacion(ReservacionInstalacion reservacionInstalacion)
        {
            _context.ReservasInstalaciones.Add(reservacionInstalacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservacionInstalacion", new { id = reservacionInstalacion.ID }, reservacionInstalacion);
        }

        // DELETE: api/ReservacionInstalacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservacionInstalacion(int id)
        {
            var reservacionInstalacion = await _context.ReservasInstalaciones.FindAsync(id);
            if (reservacionInstalacion == null)
            {
                return NotFound();
            }

            _context.ReservasInstalaciones.Remove(reservacionInstalacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ReservacionInstalacion>>> SearchReservaInsta(string? nombre_apellido)
        {
            var reservaQuery = _context.ReservasInstalaciones
                .Include(m => m.Usuario)
                .Select(r => new
                {
                    r.ID,
                    Usuario = r.Usuario.Nombre + " " + r.Usuario.Apellido,  // Concatenar el nombre y apellido
                    Instalacion = r.Instalacion.Nombre,
                    r.Instalacion.Dia,
                    r.Instalacion.HoraInicio,
                    r.Instalacion.HoraFin,
                    r.Fecha,
                    r.Disponibilidad,
                })
                .AsQueryable();

            // Filtro por nombre y apellido si se proporciona
            if (!string.IsNullOrEmpty(nombre_apellido))
            {
                reservaQuery = reservaQuery.Where(m =>
                    (m.Usuario.Contains(nombre_apellido)));  // Esto busca en la concatenación de nombre y apellido
            }

            // Filtro por fecha si se proporciona
            //if (fecha.HasValue)
            //{
            //    reservaQuery = reservaQuery.Where(m => m.Fecha == fecha.Value);  // Se usa fecha.Value para acceder al valor de DateOnly
            //}

            // Obtener los resultados
            var reservas = await reservaQuery.ToListAsync();

            // Verificar si no se encontraron reservas
            if (!reservas.Any())
            {
                return NotFound("No se encuentran reservas");
            }

            // Retornar los resultados
            return Ok(reservas);
        }


        private bool ReservacionInstalacionExists(int id)
        {
            return _context.ReservasInstalaciones.Any(e => e.ID == id);
        }
    }
}
