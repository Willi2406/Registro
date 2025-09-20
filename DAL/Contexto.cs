using Microsoft.EntityFrameworkCore;
using Registro.Models;

namespace Registro.DAL
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options){ }

        public DbSet<Jugadores> Jugadores { get; set; } = null!;
        public DbSet<Partidas> Partidas { get; set; } = null!;
        public DbSet<Movimientos> Movimientos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Partidas>(entity =>
            {
                entity.HasOne(p => p.Jugador1)
                      .WithMany()
                      .HasForeignKey(p => p.Jugador1Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Jugador2)
                      .WithMany()
                      .HasForeignKey(p => p.Jugador2Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Ganador)
                      .WithMany()
                      .HasForeignKey(p => p.GanadorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.TurnoJugador)
                      .WithMany()
                      .HasForeignKey(p => p.TurnoJugadorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Movimientos>(entity =>
            {
                entity.HasOne(m => m.Jugadores)
                      .WithMany(j => j.Movimientos)
                      .HasForeignKey(m => m.JugadorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Partida)
                      .WithMany()
                      .HasForeignKey(m => m.PartidaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
