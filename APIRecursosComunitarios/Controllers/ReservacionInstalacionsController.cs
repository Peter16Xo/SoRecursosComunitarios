﻿using System;
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

        // GET: api/ReservacionInstalacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservacionInstalacion>> GetReservacionInstalacion(int id)
        {
            var reservacionInstalacion = await _context.ReservasInstalaciones.FindAsync(id);

            if (reservacionInstalacion == null)
            {
                return NotFound();
            }

            return reservacionInstalacion;
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

        private bool ReservacionInstalacionExists(int id)
        {
            return _context.ReservasInstalaciones.Any(e => e.ID == id);
        }
    }
}
