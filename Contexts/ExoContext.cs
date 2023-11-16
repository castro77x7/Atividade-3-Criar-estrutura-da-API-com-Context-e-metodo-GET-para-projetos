using Exo.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Exo.WebApi.Contexts
{
    public class ExoContext : DbContext
    {
        public ExoContext()
        {
        }

        public ExoContext(DbContextOptions<ExoContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Connection string depends on your machine.
                //optionsBuilder.UseSqlServer("localhost\\SQLEXPRESS;Database=ExoApiNovo;Trusted_Connection=True");
                // Example connection strings:
                // User ID=sa;Password=admin;Server=localhost;Database=ExoApi;Trusted_Connection=True
                // Server=localhost\\SQLEXPRESS;Database=ExoApi;Trusted_Connection=True
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ExoApiNovo;User Id=sa;Password=1234;");
            }
        }

        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
