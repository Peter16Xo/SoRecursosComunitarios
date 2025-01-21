using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Data.Contexts;

namespace RecursosComunitariosAPI.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reporte>>> GetReportes()
        {
            return await _context.Reportes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reporte>> GetReporte(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte == null) return NotFound();
            return reporte;
        }

        [HttpPost]
        public async Task<ActionResult<Reporte>> CreateReporte(Reporte reporte)
        {
            _context.Reportes.Add(reporte);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReporte), new { id = reporte.ID }, reporte);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReporte(int id, Reporte reporte)
        {
            if (id != reporte.ID) return BadRequest();
            _context.Entry(reporte).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReporte(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte == null) return NotFound();

            _context.Reportes.Remove(reporte);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}