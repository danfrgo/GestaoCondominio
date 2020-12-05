using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Interfaces
{
    public interface IMesRepositorio : IRepositorioGenerico<Mes>
    {
        new Task<IEnumerable<Mes>> ObterTodos();
    }
}
