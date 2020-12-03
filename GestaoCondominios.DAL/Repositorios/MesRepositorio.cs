﻿using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Repositorios
{
    public class MesRepositorio : RepositorioGenerico<Mes>, IMesRepositorio
    {
        private readonly Contexto _contexto;
        public MesRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public new async Task<IEnumerable<Mes>> ObterTodos()
        {
            try
            {
                return await _contexto.Meses.OrderBy(m => m.MesId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
