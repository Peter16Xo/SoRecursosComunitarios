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
    public class ReportesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reportes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reporte>>> GetReportes()
        {
            return await _context.Reportes.ToListAsync();
        }

        // GET: api/Reportes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reporte>> GetReporte(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);

            if (reporte == null)
            {
                return NotFound();
            }

            return reporte;
        }
        //Search "buscar"
        // GET: api/Reportes/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Reporte>>> SearchReportes(string? titulo, string? recursoafectado)
        {
            var reportQuery = _context.Reportes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(titulo))
            {
                reportQuery = reportQuery.Where(r => r.Titulo.Contains(titulo));
            }
            if (!string.IsNullOrWhiteSpace(recursoafectado))
            {
                reportQuery = reportQuery.Where(r => r.RecursoAfectado.Contains(recursoafectado));
            }

            var reporte = await reportQuery.ToListAsync();
            if (!reporte.Any())
            {
                return NotFound("No se encontraron reportes con ese criterio");
            }
            return Ok(reporte);
        }
        // PUT: api/Reportes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReporte(int id, Reporte reporte)
        {
            if (id != reporte.ID)
            {
                return BadRequest();
            }

            _context.Entry(reporte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReporteExists(id))
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

        // POST: api/Reportes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reporte>> PostReporte(Reporte reporte)
        {
            _context.Reportes.Add(reporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReporte", new { id = reporte.ID }, reporte);
        }

        // DELETE: api/Reportes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReporte(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte == null)
            {
                return NotFound();
            }

            _context.Reportes.Remove(reporte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReporteExists(int id)
        {
            return _context.Reportes.Any(e => e.ID == id);
        }
    }
}
