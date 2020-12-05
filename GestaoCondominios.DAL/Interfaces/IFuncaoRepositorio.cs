using GestaoCondominios.BLL;
using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Interfaces
{
    public interface IFuncaoRepositorio : IRepositorioGenerico<Funcao>
    {
        Task AdicionarFuncao(Funcao funcao);

        new Task Atualizar(Funcao funcao);
    }
}
