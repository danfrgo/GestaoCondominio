using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Repositorios
{
    public class AluguerRepositorio : RepositorioGenerico<Aluguer>, IAluguerRepositorio
    {
        private readonly Contexto _contexto;
        public AluguerRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public bool AluguerJaExiste(int mesId, int ano)
        {
            try
            {
                return _contexto.Alugueres.Any(a => a.MesId == mesId && a.Ano == ano);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public new async Task<IEnumerable<Aluguer>> ObterTodos()
        {
            try
            {
                return await _contexto.Alugueres.Include(a => a.Mes).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /*
        public async Task<IEnumerable<int>> ObterTodosAnos()
        {
            try
            {
                return await _contexto.Alugueres.Select(a => a.Ano)
                    .Distinct()
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        */

     
    }
}
