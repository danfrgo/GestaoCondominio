using GestaoCondominios.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Interfaces
{
    public interface IVeiculoRepositorio : IRepositorioGenerico<Veiculo>
    {
        Task<IEnumerable<Veiculo>> ObterVeiculosPorUtilizador(string utilizadorId);
    }
}