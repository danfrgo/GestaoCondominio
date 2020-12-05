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
    public class VeiculosController : Controller
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;
        private readonly IUtilizadorRepositorio _utilizadorRepositorio;

        public VeiculosController(IVeiculoRepositorio veiculoRepositorio, IUtilizadorRepositorio utilizadorRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _utilizadorRepositorio = utilizadorRepositorio;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VeiculoId,Nome,Marca,Cor,Matricula,UtilizadorId")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                Utilizador utilizador = await _utilizadorRepositorio.ObterUtilizadorPeloNome(User);
                veiculo.UtilizadorId = utilizador.Id;
                await _veiculoRepositorio.Inserir(veiculo);
                return RedirectToAction("MinhasInformacoes", "Utilizadores");
            }

            return View(veiculo);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var veiculo = await _veiculoRepositorio.ObterPeloId(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return View(veiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VeiculoId,Nome,Marca,Cor,Matricula,UtilizadorId")] Veiculo veiculo)
        {
            if (id != veiculo.VeiculoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _veiculoRepositorio.Atualizar(veiculo);
                return RedirectToAction("MinhasInformacoes", "Utilizadores");
            }

            return View(veiculo);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _veiculoRepositorio.Excluir(id);
            return Json("Veículo excluído com sucesso");
        }
    }
}
