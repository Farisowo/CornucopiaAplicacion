using CornucopiaApp.Modelos;
using CornucopiaApp.Utilidades;
using Microsoft.EntityFrameworkCore;


namespace CornucopiaApp.DataAccess
{
    public class ClienteDBContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("clientes.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(col => col.IdCliente);
                entity.Property(col => col.IdCliente).IsRequired().ValueGeneratedOnAdd();
            });
        }
    }
}
