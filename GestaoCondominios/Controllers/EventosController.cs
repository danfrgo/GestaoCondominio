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
    public class EventosController : Controller
    {
        private readonly IEventoRepositorio _eventoRepositorio;
        private readonly IUtilizadorRepositorio _utilizadorRepositorio;

        public EventosController(IEventoRepositorio eventoRepositorio, IUtilizadorRepositorio utilizadorRepositorio)
        {
            _eventoRepositorio = eventoRepositorio;
            _utilizadorRepositorio = utilizadorRepositorio;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            Utilizador utilizador = await _utilizadorRepositorio.ObterUtilizadorPeloNome(User);

            if (await _utilizadorRepositorio.VerificarSeUtilizadorEstaEmFuncao(utilizador, "Morador"))
            {
                return View(await _eventoRepositorio.ObterEventosPeloId(utilizador.Id));
            }
            return View(await _eventoRepositorio.ObterTodos());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Utilizador utilizador = await _utilizadorRepositorio.ObterUtilizadorPeloNome(User);
            Evento evento = new Evento
            {
                UtilizadorId = utilizador.Id
            };

            return View(evento);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventoId,Nome,Data,UtilizadorId")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                await _eventoRepositorio.Inserir(evento);
                TempData["NovoRegisto"] = $"Evento {evento.Nome} inserido com sucesso";
                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Evento evento = await _eventoRepositorio.ObterPeloId(id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventoId,Nome,Data,UtilizadorId")] Evento evento)
        {
            if (id != evento.EventoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _eventoRepositorio.Atualizar(evento);
                TempData["Atualizacao"] = $"Evento {evento.Nome} atualizado";
                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }


        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _eventoRepositorio.Excluir(id);
            TempData["Exclusao"] = $"Evento excluído";
            return Json("Evento excluído");
        }
    }


}
