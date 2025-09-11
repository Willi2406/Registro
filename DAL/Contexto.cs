using Microsoft.EntityFrameworkCore;
using Registro.Models;

namespace Registro.DAL
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options){ }

        public DbSet<Jugadores> Jugadores { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jugadores>().ToTable("Jugadores");
            modelBuilder.Entity<Jugadores>()
                        .HasIndex(j => j.Nombres)
                        .IsUnique();
        }
    }
}
