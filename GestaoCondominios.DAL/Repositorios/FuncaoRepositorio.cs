using GestaoCondominios.BLL;
using GestaoCondominios.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.DAL.Repositorios
{
    public class FuncaoRepositorio : RepositorioGenerico<Funcao>, IFuncaoRepositorio
    {
        public FuncaoRepositorio(Contexto contexto) : base(contexto)
        {
        }
    }
}
