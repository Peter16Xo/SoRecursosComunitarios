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
    public class InstalacionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InstalacionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Instalacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instalacion>>> GetInstalacion()
        {
            return await _context.Instalacion.Where(i => i.Disponibilidad == "Disponible" || i.Disponibilidad == "Ocupada")
                .ToListAsync();
        }
        [HttpGet("disponible")]
        public async Task<ActionResult<IEnumerable<Instalacion>>>GetInstalacion_Dispo()
        {
            return await _context.Instalacion
               .Where(i => i.Disponibilidad == "Disponible")
               .ToListAsync();
        }
        // GET: api/Instalacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instalacion>> GetInstalacion(int id)
        {
            var instalacion = await _context.Instalacion.FindAsync(id);

            if (instalacion == null)
            {
                return NotFound();
            }

            return instalacion;
        }

        // PUT: api/Instalacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstalacion(int id, Instalacion instalacion)
        {
            if (id != instalacion.ID)
            {
                return BadRequest();
            }

            _context.Entry(instalacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstalacionExists(id))
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
        [HttpPut("desactive/{id}")]
        public async Task<IActionResult> DesactiveInstalacion(int id)
        {
            var instalacion = await _context.Instalacion.FindAsync(id);
            if (instalacion == null)
            {
                return NotFound("Instalacion no econtrado");
            }
            instalacion.Disponibilidad = "Ocupada";
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstalacionExists(id))
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
        [HttpPut("active/{id}")]
        public async Task<IActionResult> ActiveInstalacion(int id)
        {
            var instalacion = await _context.Instalacion.FindAsync(id);
            if (instalacion == null)
            {
                return NotFound("Instalacion no econtrado");
            }
            instalacion.Disponibilidad = "Disponible";
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstalacionExists(id))
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
        // POST: api/Instalacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Instalacion>> PostInstalacion(Instalacion instalacion)
        {
            _context.Instalacion.Add(instalacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstalacion", new { id = instalacion.ID }, instalacion);
        }

        // DELETE: api/Instalacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstalacion(int id)
        {
            var instalacion = await _context.Instalacion.FindAsync(id);
            if (instalacion == null)
            {
                return NotFound();
            }

            _context.Instalacion.Remove(instalacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //Search "buscar"
        // GET: api/Instalacion/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Instalacion>>> SearchInstalaciones(string? nombre, string? tipo, string? descripcion, string? dia, string? disponibilidad)
        {
            var installQuery = _context.Instalacion.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                installQuery = installQuery.Where(i => i.Nombre.Contains(nombre));
            }
            if (!string.IsNullOrWhiteSpace(tipo))
            {
                installQuery = installQuery.Where(i => i.Tipo.Contains(tipo));
            }
            if (!string.IsNullOrWhiteSpace(descripcion))
            {
                installQuery = installQuery.Where(i => i.Descripcion.Contains(descripcion));
            }
            if (!string.IsNullOrWhiteSpace(dia))
            {
                installQuery = installQuery.Where(i => i.Dia.Contains(dia));
            }
            if (!string.IsNullOrWhiteSpace(disponibilidad))
            {
                installQuery = installQuery.Where(i => i.Disponibilidad.Contains(disponibilidad));
            }

            var install = await installQuery.ToListAsync();
            if (!install.Any())
            {
                return NotFound("No se encontraron instalaciones con ese criterio");
            }
            return Ok(install);
        }

        private bool InstalacionExists(int id)
        {
            return _context.Instalacion.Any(e => e.ID == id);
        }
    }
}
