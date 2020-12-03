using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Repositorios
{
    class HistoricoRecursosRepositorio : RepositorioGenerico<HistoricoRecursos>, IHistoricoRecursosRepositorio
    {
        public HistoricoRecursosRepositorio(Contexto contexto) : base(contexto)
        {
        }

        public object ObterHistoricoDespesas(int ano)
        {
            throw new NotImplementedException();
        }

        public object ObterHistoricoGanhos(int ano)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> ObterTotalDespesas(int ano)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> ObterTotalGanhos(int ano)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HistoricoRecursos>> ObterUltimasMovimentacoes()
        {
            throw new NotImplementedException();
        }
    }
}
