using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Interfaces
{
    public interface IHistoricoRecursosRepositorio : IRepositorioGenerico<HistoricoRecursos>
    {
        object ObterHistoricoGanhos(int ano);
        object ObterHistoricoDespesas(int ano);
       
        public Task<decimal> ObterTotalDespesas(int ano);
        public Task<decimal> ObterTotalGanhos(int ano);

        public Task<IEnumerable<HistoricoRecursos>> ObterUltimasMovimentacoes();
        
    }
}
