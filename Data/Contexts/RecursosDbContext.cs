using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Data.Contexts
{
    public class RecursosDbContext : DbContext
    {
        public RecursosDbContext(DbContextOptions<RecursosDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
