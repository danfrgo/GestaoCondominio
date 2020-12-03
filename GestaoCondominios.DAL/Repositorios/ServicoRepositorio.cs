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
    public class ServicoRepositorio : RepositorioGenerico<Servico>, IServicoRepositorio
    {
        private readonly Contexto _contexto;
        public ServicoRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Servico>> ObterServicosPeloUtilizador(string utilizadorId)
        {
            try
            {
                return await _contexto.Servicos.Where(s => s.UtilizadorId == utilizadorId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
