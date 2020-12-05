using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestaoCondominios.Controllers
{
    [Authorize]
    public class PagamentosController : Controller
    {
        private readonly IPagamentoRepositorio _pagamentoRepositorio;
        private readonly IAluguerRepositorio _aluguerRepositorio;
        private readonly IHistoricoRecursosRepositorio _historicoRecursosRepositorio;
        private readonly IUtilizadorRepositorio _utilizadorRepositorio;

        public PagamentosController(IPagamentoRepositorio pagamentoRepositorio, IAluguerRepositorio aluguerRepositorio,
            IHistoricoRecursosRepositorio historicoRecursosRepositorio, IUtilizadorRepositorio utilizadorRepositorio)
        {
            _pagamentoRepositorio = pagamentoRepositorio;
            _aluguerRepositorio = aluguerRepositorio;
            _historicoRecursosRepositorio = historicoRecursosRepositorio;
            _utilizadorRepositorio = utilizadorRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            Utilizador utlizador = await _utilizadorRepositorio.ObterUtilizadorPeloNome(User);
            return View(await _pagamentoRepositorio.ObterPagamentoPorUtilizador(utlizador.Id));
        }

        public async Task<IActionResult> EfetuarPagamento(int id)
        {
            Pagamento pagamento = await _pagamentoRepositorio.ObterPeloId(id);
            pagamento.DataPagamento = DateTime.Now.Date;
            pagamento.Status = StatusPagamento.Pago;
            await _pagamentoRepositorio.Atualizar(pagamento);

            Aluguer aluguer = await _aluguerRepositorio.ObterPeloId(pagamento.AluguerId);

            HistoricoRecursos hr = new HistoricoRecursos
            {
                Valor = aluguer.Valor,
                MesId = aluguer.MesId,
                Dia = DateTime.Now.Day,
                Ano = aluguer.Ano,
                Tipo = Tipos.Entrada
            };

            await _historicoRecursosRepositorio.Inserir(hr);
            TempData["NovoRegisto"] = $"Pagamento no valor de {pagamento.Aluguer.Valor} realizado";
            return RedirectToAction(nameof(Index));
        }
    }
}