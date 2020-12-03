using GestaoCondominios.BLL;
using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Repositorios
{
    public class FuncaoRepositorio : RepositorioGenerico<Funcao>, IFuncaoRepositorio
    {
        private readonly RoleManager<Funcao> _gerirFuncoes;
        public FuncaoRepositorio(Contexto contexto, RoleManager<Funcao> gerirFuncoes) : base(contexto)
        {
            _gerirFuncoes = gerirFuncoes;
        }

        public async Task AdicionarFuncao(Funcao funcao)
        {
            try
            {
                funcao.Id = Guid.NewGuid().ToString();
                await _gerirFuncoes.CreateAsync(funcao);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // new pois estamos a subscrever 
        public new async Task Atualizar(Funcao funcao)
        {
            try
            {
                Funcao f = await ObterPeloId(funcao.Id);
                f.Name = funcao.Name;
                f.NormalizedName = funcao.NormalizedName;
                f.Descricao = funcao.Descricao;
                await _gerirFuncoes.UpdateAsync(f);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }




    }
}
