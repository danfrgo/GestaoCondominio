using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interface;
using GestaoCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestaoCondominios.Controllers
{
   
    public class ServicosController : Controller
    {
        private readonly IServicoRepositorio _servicoRepositorio;
        private readonly IUtilizadorRepositorio _utilizadorRepositorio;

        public ServicosController(IServicoRepositorio servicoRepositorio, IUtilizadorRepositorio utilizadorRepositorio)
        {
            _servicoRepositorio = servicoRepositorio;
            _utilizadorRepositorio = utilizadorRepositorio;
            
        }

        // GET: Servicos
        public async Task<IActionResult> Index()
        {
            Utilizador utilizador = await _utilizadorRepositorio.ObterUtilizadorPeloNome(User); // user -> para passar as informaçoes do User logado
            if (await _utilizadorRepositorio.VerificarSeUtilizadorEstaEmFuncao(utilizador, "Morador"))
            {
                return View(await _servicoRepositorio.ObterServicosPeloUtilizador(utilizador.Id));
            }

            return View(await _servicoRepositorio.ObterTodos());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Utilizador utilizador = await _utilizadorRepositorio.ObterUtilizadorPeloNome(User);
            Servico servico = new Servico
            {
                UtilizadorId = utilizador.Id
            };

            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicoId,Nome,Valor,Status,UtilizadorId")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                 servico.Status = StatusServico.Pendente; // sempre que criar um servico o status é por definiçao pendente
                await _servicoRepositorio.Inserir(servico);
                TempData["NovoRegisto"] = $"Serviço {servico.Nome} requisitado";
                return RedirectToAction(nameof(Index));
            }

            return View(servico);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            Servico servico = await _servicoRepositorio.ObterPeloId(id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicoId,Nome,Valor,Status,UtilizadorId")] Servico servico)
        {
            if (id != servico.ServicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _servicoRepositorio.Atualizar(servico);
                TempData["Atualizacao"] = $"Serviço {servico.Nome} atualizado";
                return RedirectToAction(nameof(Index));
            }
            return View(servico);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _servicoRepositorio.Excluir(id);
            TempData["Exclusao"] = $"Serviço excluído";
            return Json("Serviço excluído");
        }

    }
}
