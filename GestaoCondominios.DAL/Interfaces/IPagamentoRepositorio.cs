﻿using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Interfaces
{
    public interface IPagamentoRepositorio : IRepositorioGenerico<Pagamento>
    {
         Task<IEnumerable<Pagamento>> ObterPagamentoPorUtilizador(string utilizadorId); // O user vai ver apenas os seus pagamentos
    }
}
