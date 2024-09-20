using AgendaConsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsole.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Agenda> Compromissos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-6QPV884;Database=AgendaConsole;User ID=sa;Password=12345678;");
        }
    }
}
