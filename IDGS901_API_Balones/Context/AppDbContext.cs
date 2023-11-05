using IDGS901_API_Balones.Models;
using Microsoft.EntityFrameworkCore;

namespace IDGS901_API_Balones.Context
{
    public class AppDbContext : DbContext
    {
        private const string conectionstring = "conexion";

        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        { }

        public DbSet<Alumnos> Alumnos { get; set; }

        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuarios> Usuarios2 { get; set; }
        public DbSet<Clientes> Clientes2 { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<MateriaPrima> MateriaPrima { get; set; }
        public DbSet<Receta> Receta { get; set; }
        public DbSet<Direccion> Direccion2 { get; set; }
        public DbSet<Carrito> Carrito2 { get; set; }
        public DbSet<Venta> Venta2 { get; set; }
    }
}
