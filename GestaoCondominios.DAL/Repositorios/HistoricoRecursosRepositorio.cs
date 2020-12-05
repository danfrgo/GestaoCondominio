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
    class HistoricoRecursosRepositorio : RepositorioGenerico<HistoricoRecursos>, IHistoricoRecursosRepositorio
    {
        private readonly Contexto _contexto;
        public HistoricoRecursosRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public object ObterHistoricoDespesas(int ano)
        {
            try
            {
                return _contexto.HistoricoRecursos.Include(hr => hr.Mes)
                    .Where(hr => hr.Ano == ano && hr.Tipo == Tipos.Saida).ToList()
                    .OrderBy(hr => hr.MesId).GroupBy(hr => hr.Mes.Nome)
                    .Select(hr => new { Meses = hr.Key, Valores = hr.Sum(v => v.Valor) }); // key possui os valores de groupby mes.nome
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public object ObterHistoricoGanhos(int ano)
        {
            try
            {
                return _contexto.HistoricoRecursos.Include(hr => hr.Mes)
                    .Where(hr => hr.Ano == ano && hr.Tipo == Tipos.Entrada).ToList()
                    .OrderBy(hr => hr.MesId).GroupBy(hr => hr.Mes.Nome)
                    .Select(hr => new { Meses = hr.Key, Valores = hr.Sum(v => v.Valor) });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<decimal> ObterSomaDespesas(int ano)
        {
            try
            {
                return await _contexto.HistoricoRecursos.Include(hr => hr.Mes)
                    .Where(hr => hr.Ano == ano && hr.Tipo == Tipos.Saida)
                    .SumAsync(hr => hr.Valor);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<decimal> ObterSomaGanhos(int ano)
        {
            try
            {
                return await _contexto.HistoricoRecursos.Include(hr => hr.Mes)
                    .Where(hr => hr.Ano == ano && hr.Tipo == Tipos.Entrada)
                    .SumAsync(hr => hr.Valor);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<HistoricoRecursos>> ObterUltimasMovimentacoes()
        {
            try
            {
                return await _contexto.HistoricoRecursos.Include(hr => hr.Mes).OrderByDescending(hr => hr.HistoricoRecursosId)
                    .Take(5).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
