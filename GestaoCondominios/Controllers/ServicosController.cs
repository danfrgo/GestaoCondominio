using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using GestaoCondominios.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestaoCondominios.Controllers
{
   
    public class ServicosController : Controller
    {
        private readonly IServicoRepositorio _servicoRepositorio;
        private readonly IUtilizadorRepositorio _utilizadorRepositorio;
        private readonly IServicoPredioRepositorio _servicoPredioRepositorio;
        private readonly IHistoricoRecursosRepositorio _historicoRecursosRepositorio;

        public ServicosController(IServicoRepositorio servicoRepositorio, IUtilizadorRepositorio utilizadorRepositorio, IServicoPredioRepositorio servicoPredioRepositorio, IHistoricoRecursosRepositorio historicoRecursosRepositorio)
        {
            _servicoRepositorio = servicoRepositorio;
            _utilizadorRepositorio = utilizadorRepositorio;
            _servicoPredioRepositorio = servicoPredioRepositorio;
            _historicoRecursosRepositorio = historicoRecursosRepositorio;

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

        //[Authorize(Roles = "Administrador,Sindico")]
        [HttpGet]
        public async Task<IActionResult> AprovarServico(int id)
        {
            Servico servico = await _servicoRepositorio.ObterPeloId(id);
            ServicoAprovadoViewModel viewModel = new ServicoAprovadoViewModel
            {
                ServicoId = servico.ServicoId,
                Nome = servico.Nome
            };

            return View(viewModel);
        }

      
        // [Authorize(Roles = "Administrador,Sindico")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AprovarServico(ServicoAprovadoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Servico servico = await _servicoRepositorio.ObterPeloId(viewModel.ServicoId);
                servico.Status = StatusServico.Aceite;
                await _servicoRepositorio.Atualizar(servico);

                ServicoPredio servicoPredio = new ServicoPredio
                {
                    ServicoId = viewModel.ServicoId,
                    DataExecucao = viewModel.Data
                };

                await _servicoPredioRepositorio.Inserir(servicoPredio); // gravar na BD

                HistoricoRecursos hr = new HistoricoRecursos
                {
                    Valor = servico.Valor,
                    MesId = servicoPredio.DataExecucao.Month,
                    Dia = servicoPredio.DataExecucao.Day,
                    Ano = servicoPredio.DataExecucao.Year,
                    Tipo = Tipos.Saida
                };

                await _historicoRecursosRepositorio.Inserir(hr); // gravar na BD
                TempData["NovoRegisto"] = $"{servico.Nome} aprovado com sucesso";

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // [Authorize(Roles = "Administrador,Responsavel")]
        public async Task<IActionResult> RecusarServico(int id)
        {
            Servico servico = await _servicoRepositorio.ObterPeloId(id);
            if (servico == null)
                return NotFound();

            servico.Status = StatusServico.Recusado;
            await _servicoRepositorio.Atualizar(servico);
            TempData["Exclusao"] = $"{servico.Nome} recusado";

            return RedirectToAction(nameof(Index));
        }


    }
}
