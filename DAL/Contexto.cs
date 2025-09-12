using Microsoft.EntityFrameworkCore;
using Registro.Models;

namespace Registro.DAL
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options){ }

        public DbSet<Jugadores> Jugadores { get; set; } = null!;
        public DbSet<Partidas> Partidas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Partidas>(entity =>
            {
                entity.HasOne(p => p.Jugador1)
                .WithMany()
                .HasForeignKey(p => p.Jugador1Id)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Jugador2)
                .WithMany()
                .HasForeignKey(p => p.Jugador2Id)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.TurnoJugador)
                .WithMany()
                .HasForeignKey(p => p.TurnoJugadorId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Jugadores>().ToTable("Jugadores");
            modelBuilder.Entity<Jugadores>()
                        .HasIndex(j => j.Nombres)
                        .IsUnique();
        }

    }
}
