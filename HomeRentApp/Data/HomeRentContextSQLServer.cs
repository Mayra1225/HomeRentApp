using HomeRentApp.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HomeRentApp.Data
{
    public class HomeRentContextSQLServer : DbContext
    {
        public HomeRentContextSQLServer(DbContextOptions<HomeRentContextSQLServer> options)
        : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; } = default!; //Agregar las tablas de los datos 
        public DbSet<Departamento> Departamento { get; set; } = default!;

    }
}
