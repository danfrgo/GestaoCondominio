using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Repositorios
{
    interface IAluguerRepositorio : IRepositorioGenerico<Aluguer>
    {
        public interface IluguerRepositorio : IRepositorioGenerico<Aluguer>
        {
            bool AluguerJaExiste(int mesId, int ano);

            new Task<IEnumerable<Aluguer>> ObterTodos();

            Task<IEnumerable<int>> ObterTodosAnos();
        }
    }
}