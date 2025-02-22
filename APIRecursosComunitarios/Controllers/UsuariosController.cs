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
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> GetUsuarios()
        {
            return await _context.Usuarios
                .Where(u=>u.Active=='Y')
                .Select(static u => new
                {
                    u.Id,
                    u.Nombre,
                    u.Cedula,
                    u.Apellido,
                    u.Correo,
                    u.Telefono,
                    u.Active,
                })
                .ToListAsync();
        }
        [HttpPut("desactive/{id}")]
        public async Task<IActionResult>DesactiveUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) {
                return NotFound("Usuario no econtrado");
            }
            usuario.Active = 'N';
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!UsuarioExists(id))
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
        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Usuario>>> SearchReportes(string? nombre, string? apellido,string? cedula)
        {
            var userQuery = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                userQuery = userQuery.Where(r => r.Nombre.Contains(nombre));
            }
            if (!string.IsNullOrWhiteSpace(apellido))
            {
                userQuery = userQuery.Where(r => r.Apellido.Contains(apellido));
            }
            if (!string.IsNullOrWhiteSpace(cedula))
            {
                userQuery=userQuery.Where(r=>r.Cedula.Contains(cedula));
            }
            var reporte = await userQuery.ToListAsync();
            if (!reporte.Any())
            {
                return NotFound("No se encontraron reportes con ese criterio");
            }
            return Ok(reporte);
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
