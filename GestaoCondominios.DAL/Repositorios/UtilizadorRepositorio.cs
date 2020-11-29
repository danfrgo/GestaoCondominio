using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.DAL.Repositorios
{
    public class UtilizadorRepositorio : RepositorioGenerico<Utilizador>, IUtilizadorRepositorio
    {
        public UtilizadorRepositorio(Contexto contexto) : base(contexto)
        {
        }
    }
}
