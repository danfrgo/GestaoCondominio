using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestaoCondominios.Controllers
{
    [Authorize(Roles = "Administrador,Responsavel")]
    public class AlugueresController : Controller
    {


        private readonly IAluguerRepositorio _aluguerRepositorio;
        private readonly IUtilizadorRepositorio _utilizadorRepositorio;
        private readonly IPagamentoRepositorio _pagamentoRepositorio;
        private readonly IMesRepositorio _mesRepositorio;


        public AlugueresController(IAluguerRepositorio aluguerRepositorio, IUtilizadorRepositorio utilizadorRepositorio,
            IPagamentoRepositorio pagamentoRepositorio, IMesRepositorio mesRepositorio)
        {
            _aluguerRepositorio = aluguerRepositorio;
            _utilizadorRepositorio = utilizadorRepositorio;
            _pagamentoRepositorio = pagamentoRepositorio;
            _mesRepositorio = mesRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _aluguerRepositorio.ObterTodos());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["MesId"] = new SelectList(await _mesRepositorio.ObterTodos(), "MesId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AluguerId,Valor,MesId,Ano")] Aluguer aluguer)
        {
            if (ModelState.IsValid)
            {
                // verificar se o aluguer já nao está registado
                if (!_aluguerRepositorio.AluguerJaExiste(aluguer.MesId, aluguer.Ano))
                { // se nao nenhum erro, regista o novo aluguer 
                    await _aluguerRepositorio.Inserir(aluguer);
                    IEnumerable<Utilizador> utilizadores = await _utilizadorRepositorio.ObterTodos();
                    Pagamento pagamento;
                    List<Pagamento> pagamentos = new List<Pagamento>();

                    // percorrer a lista de utilizadores para obter o ID de cada um e ir adicionando ao pagamento
                    foreach (Utilizador u in utilizadores)
                    {
                        pagamento = new Pagamento
                        {
                            AluguerId = aluguer.AluguerId,
                            UtilizadorId = u.Id,
                            DataPagamento = null, // null porque o pagamento nao foi efetuado
                            Status = StatusPagamento.Pendente
                        };

                        pagamentos.Add(pagamento); // add pagamento á lista de pagamentos
                    }

                    await _pagamentoRepositorio.Inserir(pagamentos); // save na BD
                    TempData["NovoRegisto"] = $"O aluguer de valor {aluguer.Valor} € do mês {aluguer.MesId} ano {aluguer.Ano} adicionado";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Aluguer já existe");
                    ViewData["MesId"] = new SelectList(await _mesRepositorio.ObterTodos(), "MesId", "Nome", aluguer.MesId);
                    return View(aluguer);
                }

            }
            ViewData["MesId"] = new SelectList(await _mesRepositorio.ObterTodos(), "MesId", "Nome", aluguer.MesId);
            return View(aluguer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            Aluguer aluguer = await _aluguerRepositorio.ObterPeloId(id);
            if (aluguer == null)
            {
                return NotFound();
            }
            ViewData["MesId"] = new SelectList(await _mesRepositorio.ObterTodos(), "MesId", "Nome", aluguer.MesId);
            return View(aluguer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AluguerId,Valor,MesId,Ano")] Aluguer aluguer)
        {
            if (id != aluguer.AluguerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _aluguerRepositorio.Atualizar(aluguer);
                TempData["Atualizacao"] = $"Aluguer do mês {aluguer.MesId} ano {aluguer.Ano} atualizado";
                return RedirectToAction(nameof(Index));
            }
            ViewData["MesId"] = new SelectList(await _mesRepositorio.ObterTodos(), "MesId", "Nome", aluguer.MesId);
            return View(aluguer);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _aluguerRepositorio.Excluir(id);
            TempData["Exclusao"] = $"Aluguer excluído";
            return Json("Aluguer excluído");
        }
    }
}