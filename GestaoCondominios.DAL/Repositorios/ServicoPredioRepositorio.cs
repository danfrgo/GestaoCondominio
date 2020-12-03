using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Repositorios
{
    public class ServicoPredioRepositorio : RepositorioGenerico<ServicoPredio>, IServicoPredioRepositorio
    {
        public ServicoPredioRepositorio(Contexto contexto) : base(contexto)
        {
        }
    }
}
