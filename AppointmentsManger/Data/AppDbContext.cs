using AppointmentsManger.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace AppointmentsManger.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Here we define db table DbSet<Model>
        public DbSet<Appointement> Appointements { get; set; }

    }
}
