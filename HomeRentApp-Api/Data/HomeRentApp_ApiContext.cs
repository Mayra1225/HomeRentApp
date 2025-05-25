using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HomeRentApp_Api.Models;

namespace HomeRentApp_Api.Data
{
    public class HomeRentApp_ApiContext : DbContext
    {
        public HomeRentApp_ApiContext (DbContextOptions<HomeRentApp_ApiContext> options)
            : base(options)
        {
        }

        public DbSet<HomeRentApp_Api.Models.Usuario> Usuario { get; set; } = default!;
        public DbSet<HomeRentApp_Api.Models.Departamento> Departamento { get; set; } = default!;
    }
}
