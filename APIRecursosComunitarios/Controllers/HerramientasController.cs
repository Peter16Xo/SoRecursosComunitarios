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
    public class HerramientasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HerramientasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Herramientas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Herramienta>>> GetHerramienta()
        {
            return await _context.Herramienta.ToListAsync();
        }
        [HttpGet("Cantidad")]
        public async Task<ActionResult<IEnumerable<Herramienta>>> GetHerramientaCantidad()
        {
            return await _context.Herramienta.Where(h=>h.Cantidad>0).ToListAsync();
        }
        // GET: api/Herramientas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Herramienta>> GetHerramienta(int id)
        {
            var herramienta = await _context.Herramienta.FindAsync(id);

            if (herramienta == null)
            {
                return NotFound();
            }

            return herramienta;
        }

        // PUT: api/Herramientas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHerramienta(int id, Herramienta herramienta)
        {
            if (id != herramienta.ID)
            {
                return BadRequest();
            }

            _context.Entry(herramienta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HerramientaExists(id))
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

        // POST: api/Herramientas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Herramienta>> PostHerramienta(Herramienta herramienta)
        {
            _context.Herramienta.Add(herramienta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHerramienta", new { id = herramienta.ID }, herramienta);
        }

        // DELETE: api/Herramientas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHerramienta(int id)
        {
            var herramienta = await _context.Herramienta.FindAsync(id);
            if (herramienta == null)
            {
                return NotFound();
            }

            _context.Herramienta.Remove(herramienta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("aumentar_cantidad{id}")]
        public async Task<IActionResult>aumentarCantidad(int id){
            var herramienta = await _context.Herramienta.FindAsync(id);
            if (herramienta == null)
            {
                return NotFound("Herramienta no econtrado");
            }
            herramienta.Cantidad = herramienta.Cantidad + 1;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HerramientaExists(id))
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
        [HttpPut("menor_cantidad{id}")]
        public async Task<IActionResult>menorCantidad(int id){
            var herramienta = await _context.Herramienta.FindAsync(id);
            if(herramienta == null){
                return NotFound("Herramienta no econtrado");
            }
            herramienta.Cantidad = herramienta.Cantidad - 1;
            try{
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!HerramientaExists(id)){
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
        public async Task<IActionResult> DesactiveHerramienta(int id)
        {
            var herramienta = await _context.Herramienta.FindAsync(id);
            if (herramienta == null)
            {
                return NotFound("Herramienta no econtrado");
            }
            herramienta.Disponibilidad = "Ocupada";
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HerramientaExists(id))
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
        //Search "buscar"
        // GET: api/Herramientas/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Herramienta>>> SearchHerramientas(string? nombre, string? ubicacion, string? descripcion, string? disponibilidad)
        {
            var toolQuery = _context.Herramienta.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                toolQuery = toolQuery.Where(t => t.Nombre.Contains(nombre));
            }
            if (!string.IsNullOrWhiteSpace(ubicacion))
            {
                toolQuery = toolQuery.Where(t => t.Ubicacion.Contains(ubicacion));
            }
            if (!string.IsNullOrWhiteSpace(descripcion))
            {
                toolQuery = toolQuery.Where(t => t.Descripcion.Contains(descripcion));
            }
            if (!string.IsNullOrWhiteSpace(disponibilidad))
            {
                toolQuery = toolQuery.Where(t => t.Disponibilidad.Contains(disponibilidad));
            }

            var tool = await toolQuery.ToListAsync();
            if (!tool.Any())
            {
                return NotFound("No se encontraron herramientas con ese criterio");
            }
            return Ok(tool);
        }
        private bool HerramientaExists(int id)
        {
            return _context.Herramienta.Any(e => e.ID == id);
        }
    }
}
