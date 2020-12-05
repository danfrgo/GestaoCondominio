using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Interfaces
{
    public interface IAluguerRepositorio : IRepositorioGenerico<Aluguer>
    {
        bool AluguerJaExiste(int mesId, int ano);

        new Task<IEnumerable<Aluguer>> ObterTodos();

        // Task<IEnumerable<int>> ObterTodosAnos();
    }
}
