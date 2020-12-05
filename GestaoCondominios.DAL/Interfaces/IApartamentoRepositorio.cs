using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Interfaces
{
    public interface IApartamentoRepositorio : IRepositorioGenerico<Apartamento>
    {
        new Task<IEnumerable<Apartamento>> ObterTodos();

        Task<IEnumerable<Apartamento>> ObterApartamentoPeloUtilizador(string utilizadorId);
    }
}
