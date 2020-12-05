using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoCondominios.BLL;
using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestaoCondominios.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class FuncoesController : Controller
    {
        private readonly IFuncaoRepositorio _funcaoRepositorio;

        public FuncoesController(IFuncaoRepositorio funcaoRepositorio)
        {
            _funcaoRepositorio = funcaoRepositorio;
        }

        // GET: Funcoes
        public async Task<IActionResult> Index()
        {
            return View(await _funcaoRepositorio.ObterTodos());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Id,Name,NormalizedName,ConcurrencyStamp")] Funcao funcao)
        {
            if (ModelState.IsValid)
            {
                await _funcaoRepositorio.AdicionarFuncao(funcao);
                TempData["NovoRegisto"] = $"Função {funcao.Name} adicionada";
                return RedirectToAction(nameof(Index));
            }
            return View(funcao);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            Funcao funcao = await _funcaoRepositorio.ObterPeloId(id);
            if (funcao == null)
            {
                return NotFound();
            }
            return View(funcao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Descricao,Id,Name,NormalizedName,ConcurrencyStamp")] Funcao funcao)
        {
            if (id != funcao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _funcaoRepositorio.Atualizar(funcao);
                TempData["Atualizacao"] = $"Função {funcao.Name} atualizada";
                return RedirectToAction(nameof(Index));
            }
            return View(funcao);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            await _funcaoRepositorio.Excluir(id);
            TempData["Exclusao"] = $"Função excluída";
            return Json("Função excluída");
        }
    }
}