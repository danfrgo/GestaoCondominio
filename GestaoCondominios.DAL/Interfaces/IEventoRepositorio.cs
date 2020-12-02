using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Interfaces
{
    public interface IEventoRepositorio : IRepositorioGenerico<Evento>
    {
        Task<IEnumerable<Evento>> ObterEventosPeloId(string utilizadorID);
    }
}
