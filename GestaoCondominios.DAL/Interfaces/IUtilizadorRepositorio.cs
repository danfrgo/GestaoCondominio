﻿using GestaoCondominios.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Interface
{
    public interface IUtilizadorRepositorio: IRepositorioGenerico<Utilizador>
    {
        int VerificarSeExisteRegisto(); // se existe registos na tabela Utilizadores
        Task LoginUtilizador(Utilizador utilizador, bool lembrar);

        Task LogoutUtilizador();
        Task<IdentityResult> CriarUtilizador(Utilizador utilizador, string password);
        Task IncluirUtilizadorEmFuncao(Utilizador utilizador, string funcao); // o primeiro utilizador será o Administrador, e será através deste metodo que ele será incluido

        Task<Utilizador> ObterUtilizadorPeloEmail(string email);

        Task AtualizarUtilizador(Utilizador utilizador);

        Task<bool> VerificarSeUtilizadorEstaEmFuncao(Utilizador utilizador, string funcao);

        Task<IList<string>> ObterFuncoesUtilizador(Utilizador utilizador);
        Task<IdentityResult> RemoverFuncoesUtilizador(Utilizador utilizador, IEnumerable<string> funcoes);

        Task<IdentityResult> IncluirUtilizadorEmFuncoes(Utilizador utilizador, IEnumerable<string> funcoes);

        Task<Utilizador> ObterUtilizadorPeloNome(ClaimsPrincipal utilizador);

        Task<Utilizador> ObterUtilizadorPeloId(string utilizadorId);

        string CodificarPassword(Utilizador utilizador, string password);
    }
}