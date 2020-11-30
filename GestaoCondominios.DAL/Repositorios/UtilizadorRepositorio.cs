using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace GestaoCondominios.DAL.Repositorios
{
    public class UtilizadorRepositorio : RepositorioGenerico<Utilizador>, IUtilizadorRepositorio
    {
        private readonly Contexto _contexto;
        private readonly UserManager<Utilizador> _gestorUtilizadores; // gerir as operaçoes dos Users
        private readonly SignInManager<Utilizador> _gestorLogin;
        public UtilizadorRepositorio(Contexto contexto, UserManager<Utilizador> gestorUtilizadores, SignInManager<Utilizador> gestorLogin) : base(contexto)
        {
            // injeçao de dependencias
            _contexto = contexto;
            _gestorUtilizadores = gestorUtilizadores;
            _gestorLogin = gestorLogin;
        }

        public async Task<IdentityResult> CriarUtilizador(Utilizador utilizador, string password)
        {
            try
            {
                return await _gestorUtilizadores.CreateAsync(utilizador, password);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task IncluirUtilizadorEmFuncao(Utilizador utilizador, string funcao)
        {
            try
            {
                await _gestorUtilizadores.AddToRoleAsync(utilizador, funcao);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task LoginUtilizador(Utilizador utilizador, bool lembrar)
        {
            try
            {
                await _gestorLogin.SignInAsync(utilizador, lembrar);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task LogoutUtilizador()
        {
            try
            {
                await _gestorLogin.SignOutAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int VerificarSeExisteRegisto()
        {
            try
            {
                return _contexto.Utilizadores.Count(); // tabela Utilizadores
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<Utilizador> ObterUtilizadorPeloEmail(string email)
        {
            try
            {
                return await _gestorUtilizadores.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task AtualizarUtilizador (Utilizador utilizador)
        {
            try
            {
                await _gestorUtilizadores.UpdateAsync(utilizador);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string CodificarPassword(Utilizador utilizador, string password)
        {
            throw new NotImplementedException();
        }


        public Task<IdentityResult> IncluirUtilizadorEmFuncoes(Utilizador utilizador, IEnumerable<string> funcoes)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> ObterFuncoesUtilizador(Utilizador utilizador)
        {
            throw new NotImplementedException();
        }  

        public Task<Utilizador> ObterUtilizadorPeloId(string utilizadorId)
        {
            throw new NotImplementedException();
        }

        public Task<Utilizador> ObterUtilizadorPeloNome(ClaimsPrincipal utilizador)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> RemoverFuncoesUtilizador(Utilizador utilizador, IEnumerable<string> funcoes)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerificarSeUtilizadorEstaEmFuncao(Utilizador utilizador, string funcao)
        {
            throw new NotImplementedException();
        }
    }
}
