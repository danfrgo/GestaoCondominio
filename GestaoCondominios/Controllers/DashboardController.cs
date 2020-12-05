using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoCondominios.DAL.Interfaces;
using GestaoCondominios.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestaoCondominios.Controllers
{
    //[Authorize(Roles = "Administrador,Responsavel")]
    public class DashboardController : Controller
    {
        private readonly IAluguerRepositorio _aluguerRepositorio;
        private readonly IHistoricoRecursosRepositorio _historicoRecursosRepositorio;

        public DashboardController(IAluguerRepositorio aluguerRepositorio, IHistoricoRecursosRepositorio historicoRecursosRepositorio)
        {
            _aluguerRepositorio = aluguerRepositorio;
            _historicoRecursosRepositorio = historicoRecursosRepositorio;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Anos"] = new SelectList(await _aluguerRepositorio.ObterTodosAnos());
            return View();
        }

        public JsonResult DadosGraficoGanhos(int ano)
        {
            return Json(_historicoRecursosRepositorio.ObterHistoricoGanhos(ano));
        }

        public JsonResult DadosGraficoDespesas(int ano)
        {
            return Json(_historicoRecursosRepositorio.ObterHistoricoDespesas(ano));
        }

        public async Task<JsonResult> DadosGraficoDespesasGanhosTotais()
        {
            int ano = DateTime.Now.Year;
            GanhosDespesasViewModel model = new GanhosDespesasViewModel
            {
                Despesas = await _historicoRecursosRepositorio.ObterTotalDespesas(ano),
                Ganhos = await _historicoRecursosRepositorio.ObterTotalGanhos(ano)
            };

            return Json(model);

        }
     
    }
}
