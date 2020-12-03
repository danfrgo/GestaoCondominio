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
    public class ApartamentoRepositorio : RepositorioGenerico<Apartamento>, IApartamentoRepositorio
    {
        private readonly Contexto _contexto;
        public ApartamentoRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Apartamento>> ObterApartamentoPeloUtilizador(string utilizadorId)
        {
            try
            {
                return await _contexto.Apartamentos
                    .Include(a => a.Morador).Include(a => a.Proprietario) // Include -> pois vou precisar de informações de outras tabelas
                    .Where(a => a.MoradorId == utilizadorId || a.ProprietarioId == utilizadorId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public new async Task<IEnumerable<Apartamento>> ObterTodos()
        {
            try
            {
                return await _contexto.Apartamentos.Include(a => a.Morador).Include(a => a.Proprietario).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
