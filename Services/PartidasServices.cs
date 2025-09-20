using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Registro.DAL;
using Registro.Models;

namespace Registro.Services
{
    public class PartidasServices(IDbContextFactory<Contexto> dbContextFactory)
    {
        private readonly IDbContextFactory<Contexto> _factory = dbContextFactory;

        public async Task<bool> Existe(int id)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            return await ctx.Partidas.AnyAsync(e => e.PartidaId == id);
        }

        public async Task<bool> Insertar(Partidas partidas)
        {
            using var ctx = await _factory.CreateDbContextAsync();

            if (partidas.PartidaId != 0 && await ctx.Partidas.AnyAsync(p => p.PartidaId == partidas.PartidaId))
                throw new InvalidOperationException("Ya existe una partida con ese Id.");

            ctx.Partidas.Add(partidas);
            return await ctx.SaveChangesAsync() > 0;

        }
        public async Task<bool> Modificar(Partidas partidas)
        {
            using var ctx = await _factory.CreateDbContextAsync();

            if (!await ctx.Partidas.AsNoTracking().AnyAsync(p => p.PartidaId == partidas.PartidaId))
                throw new KeyNotFoundException("No existe la partida que intentas modificar.");

            ctx.Entry(partidas).State = EntityState.Modified;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<Partidas?> Buscar(int id)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            return await ctx.Partidas.FindAsync(id);
        }

        public async Task<bool> Eliminar(int id)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            var entidad = await ctx.Partidas.FindAsync(id);
            if (entidad is null) return false;

            ctx.Partidas.Remove(entidad);
            return await ctx.SaveChangesAsync() > 0;
        }
        public async Task<bool> Guardar(Partidas partidas) => await Existe(partidas.PartidaId) ? await Modificar(partidas) : await Insertar(partidas);

        public async Task<List<Partidas>> Listar(Expression<Func<Partidas, bool>> criterio)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            return await ctx.Partidas
                .Include(p => p.Jugador1)
                .Include(p => p.Jugador2)
                .Include(p => p.TurnoJugador)
                .Include(p => p.Ganador)
                .Where(criterio)
                .OrderBy(e => e.PartidaId)
                .ToListAsync();
        }
    }
}