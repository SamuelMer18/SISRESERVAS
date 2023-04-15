using Microsoft.EntityFrameworkCore;
using SISRESERVAS.Models;
using System.Drawing;


namespace SISRESERVAS.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<departamento> Departamentos { get; set; }
        public DbSet<bus> Buses { get; set; }
        public DbSet<reserva> Reservas { get; set; }
        public DbSet<usuario> Usuarios { get; set; }
        public DbSet<chofer> Chofer { get; set; }


    }
}

