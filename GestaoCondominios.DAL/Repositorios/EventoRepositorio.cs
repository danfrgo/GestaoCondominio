using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Repositorios
{
    public class EventoRepositorio : RepositorioGenerico<Evento>, IEventoRepositorio
    {
        private readonly Contexto _contexto;
        public EventoRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }


        public async Task<IEnumerable<Evento>> ObterEventosPeloId(string utilizadorId)
        {
            try
            {
                return await _contexto.Eventos.Where(e => e.UtilizadorId == utilizadorId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
