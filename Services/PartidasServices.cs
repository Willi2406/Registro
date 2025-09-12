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
            using var ctz = await _factory.CreateDbContextAsync();
            return await ctz.Partidas.AnyAsync(e => e.JugadorId == id);
        }

        public async Task<bool> Guardar(Partidas partidas) => await 

       

    }
}