using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Registro.DAL;
using Registro.Models;

namespace Registro.Services
{
    public class JugadoresServices(IDbContextFactory<Contexto> dbContextFactory)
    {


        private readonly IDbContextFactory<Contexto> _factory = dbContextFactory;

        public async Task<bool> Guardar(Jugadores jugador)
            => await Existe(jugador.JugadorId) ? await Modificar(jugador) : await Insertar(jugador);


        public async Task<bool> Existe(int id)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            return await ctx.Jugadores.AnyAsync(e => e.JugadorId == id);
        }


        public async Task<bool> Insertar(Jugadores jugador)
        {
            using var ctx = await _factory.CreateDbContextAsync();

            if (await ctx.Jugadores.AnyAsync(j => j.Nombres == jugador.Nombres))
                throw new InvalidOperationException("Ya existe un jugador con ese nombre.");

            ctx.Jugadores.Add(jugador);
            return await ctx.SaveChangesAsync() > 0;
        }


        public async Task<bool> Modificar(Jugadores jugador)
        {
            using var ctx = await _factory.CreateDbContextAsync();

            if (await ctx.Jugadores.AnyAsync(j =>
                j.Nombres == jugador.Nombres && j.JugadorId != jugador.JugadorId))
                throw new InvalidOperationException("Ya existe un jugador con ese nombre.");

            ctx.Entry(jugador).State = EntityState.Modified;
            return await ctx.SaveChangesAsync() > 0;
        }


        public async Task<Jugadores?> Buscar(int id)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            return await ctx.Jugadores.FindAsync(id);
        }


        public async Task<bool> Eliminar(int id)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            var entidad = await ctx.Jugadores.FindAsync(id);
            if (entidad is null) return false;

            ctx.Jugadores.Remove(entidad);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task RegistrarVictoria(int ganadorId, int perdedorId)
        {
            var gan = await Buscar(ganadorId);
            var per = await Buscar(perdedorId);
            if (gan is null || per is null) return;

            gan.Victorias += 1;
            per.Derrotas += 1;
            await Guardar(gan);
            await Guardar(per);
        }

        public async Task RegistrarEmpate(int jugador1Id, int jugador2Id)
        {
            var j1 = await Buscar(jugador1Id);
            var j2 = await Buscar(jugador2Id);
            if (j1 is null || j2 is null) return;

            j1.Empate += 1;
            j2.Empate += 1;
            await Guardar(j1);
            await Guardar(j2);
        }


        public async Task<List<Jugadores>> Listar(Expression<Func<Jugadores, bool>> criterio)
        {
            using var ctx = await _factory.CreateDbContextAsync();
            return await ctx.Jugadores
                            .Where(criterio)
                            .OrderBy(e => e.Nombres)
                            .ToListAsync();
        }
    }
}
