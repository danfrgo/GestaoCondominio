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
    public class PagamentoRepositorio : RepositorioGenerico<Pagamento>, IPagamentoRepositorio
    {
        
        private readonly Contexto _contexto;
        public PagamentoRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        
        public async Task<IEnumerable<Pagamento>> ObterPagamentoPorUtilizador(string utilizadorId)
        {
            try
            {
                return await _contexto.Pagamentos.Include(p => p.Aluguer).ThenInclude(p => p.Mes)
                    .Where(p => p.UtilizadorId == utilizadorId).ToListAsync(); // filtrar pelo ID do user
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
    }
}