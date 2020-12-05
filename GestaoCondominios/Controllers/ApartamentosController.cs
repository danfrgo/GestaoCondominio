using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestaoCondominios.Controllers
{
    // [Authorize(Roles = "Administrador,Sindico")]
    public class ApartamentosController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment; // pois o apartamento possui imagens
        private readonly IApartamentoRepositorio _apartamentoRepositorio;
        private readonly IUtilizadorRepositorio _utilizadorRepositorio;

        public ApartamentosController(IWebHostEnvironment webHostEnvironment, IApartamentoRepositorio apartamentoRepositorio, IUtilizadorRepositorio utilizadorRepositorio)
        {
            _webHostEnvironment = webHostEnvironment;
            _utilizadorRepositorio = utilizadorRepositorio;
            _apartamentoRepositorio = apartamentoRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            Utilizador utilizador = await _utilizadorRepositorio.ObterUtilizadorPeloNome(User); // obter utilizador logado, o User -> contem todas as informaçoes do utilizador

            // se o user for Responsavel ele consegue ver todos os apartamentos registados
            if (await _utilizadorRepositorio.VerificarSeUtilizadorEstaEmFuncao(utilizador, "Responsavel"))
            {
                return View(await _apartamentoRepositorio.ObterTodos());
            }

            return View(await _apartamentoRepositorio.ObterApartamentoPeloUtilizador(utilizador.Id));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // lista com o valor do Id do controler e o nome do utilizador
            ViewData["MoradorId"] = new SelectList(await _utilizadorRepositorio.ObterTodos(), "Id", "UserName");
            ViewData["ProprietarioId"] = new SelectList(await _utilizadorRepositorio.ObterTodos(), "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartamentoId,Numero,Andar,Foto,MoradorId,ProprietarioId")] Apartamento apartamento, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                // verificar se tem alguma foto selecionada
                if (foto != null)
                {
                    string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens"); ///wwwwroot, pasta Imagens
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    // FileStream para gravar a foto no diretorio com o nome atribuido
                    using (FileStream fileStream = new FileStream(Path.Combine(diretorio, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream); // inserir foto no diretorio
                        apartamento.Foto = "~/Imagens/" + nomeFoto;
                    }
                }

                await _apartamentoRepositorio.Inserir(apartamento);
                TempData["NovoRegisto"] = $"Apartamento número {apartamento.Numero} registado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            // caso os dados sejam invalidos
            ViewData["MoradorId"] = new SelectList(await _utilizadorRepositorio.ObterTodos(), "Id", "UserName", apartamento.MoradorId); // lista com o valor do Id do controler e o nome do utilizador
            ViewData["ProprietarioId"] = new SelectList(await _utilizadorRepositorio.ObterTodos(), "Id", "UserName", apartamento.ProprietarioId); // lista com o valor do Id do controler e o nome do utilizador
            return View(apartamento);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            Apartamento apartamento = await _apartamentoRepositorio.ObterPeloId(id);
            if (apartamento == null)
            {
                return NotFound();
            }

            TempData["Foto"] = apartamento.Foto; // Tempdata para armazenar o diretorio da foto, se o user nao escolher outra foto, nao se perde a foto atual
            // caso exista dados retorna  a view com o model apartamento
            ViewData["MoradorId"] = new SelectList(await _utilizadorRepositorio.ObterTodos(), "Id", "UserName", apartamento.MoradorId); // lista com o valor do Id do controler e o nome do utilizador
            ViewData["ProprietarioId"] = new SelectList(await _utilizadorRepositorio.ObterTodos(), "Id", "UserName", apartamento.ProprietarioId); // lista com o valor do Id do controler e o nome do utilizador
            return View(apartamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApartamentoId,Numero,Andar,Foto,MoradorId,ProprietarioId")] Apartamento apartamento, IFormFile foto)
        {
            if (id != apartamento.ApartamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorio, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        apartamento.Foto = "~/Imagens/" + nomeFoto;
                        // quando escolho nova foto, a anterior é substituida
                        System.IO.File.Delete(TempData["Foto"].ToString().Replace("~", "wwwroot"));
                    }
                }
                else
                    // quando o user nao escolhe nada a foto atual é preservada
                    apartamento.Foto = TempData["Foto"].ToString();

                await _apartamentoRepositorio.Atualizar(apartamento); // atualizar apartamento
                TempData["Atualizacao"] = $"Apartamento número {apartamento.Numero} atualizado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            // caso os dados nao sejam validos , retorna o user para a view
            ViewData["MoradorId"] = new SelectList(await _utilizadorRepositorio.ObterTodos(), "Id", "UserName", apartamento.MoradorId);
            ViewData["ProprietarioId"] = new SelectList(await _utilizadorRepositorio.ObterTodos(), "Id", "UserName", apartamento.ProprietarioId);
            return View(apartamento);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            Apartamento apartamento = await _apartamentoRepositorio.ObterPeloId(id);
            System.IO.File.Delete(apartamento.Foto.Replace("~", "wwwroot")); // remover a foto do apartamento
            await _apartamentoRepositorio.Excluir(apartamento);
            TempData["Exclusao"] = $"Apartamento número {apartamento.Numero} excluído com sucesso";
            return Json("Apartamento excluído com sucesso");
        }

    }
}
