using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GestaoCondominios.BLL;
using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interfaces;
using GestaoCondominios.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestaoCondominios.Controllers
{
    public class UtilizadoresController : Controller
    {
        //injeçao de dependencia

        private readonly IUtilizadorRepositorio _utilizadorRepositorio;
        private readonly IFuncaoRepositorio _funcaoRepositorio;
        private readonly IWebHostEnvironment _webHostEnvironment;// para conseguir gravar a foto num diretorio

        public UtilizadoresController(IUtilizadorRepositorio utilizadorRepositorio, IFuncaoRepositorio funcaoRepositorio, IWebHostEnvironment webHostEnvironment)
        {
            _utilizadorRepositorio = utilizadorRepositorio;
            _funcaoRepositorio = funcaoRepositorio;
            _webHostEnvironment = webHostEnvironment; // as imagens vao ficar no diretorio imagens
        }

        [Authorize(Roles = "Administrador,Responsavel")]
        public async Task<IActionResult> Index()
        {
            return  View(await _utilizadorRepositorio.ObterTodos());
        }

        [AllowAnonymous] // nao precisa de login para executar a funcao
        [HttpGet]
        public IActionResult Registo()
        {
            return View();
        }

        [AllowAnonymous] // nao precisa de login para executar a funcao
        [ValidateAntiForgeryToken] // para evitar que se falsifique o envio de dados para o servidor
        [HttpPost]
        public async Task<IActionResult> Registo(RegistoViewModel model, IFormFile foto) // IFormFile para gravar a foto num directorio
        {
            if (ModelState.IsValid)
            {
                if(foto != null)
                {
                    string diretorioPasta = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorioPasta, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        model.Foto = "~/Imagens/" + nomeFoto;
                    }
                }

                Utilizador utilizador = new Utilizador();
                IdentityResult utilizadorCriado;

                // no caso de nao haver users registados
                // se o repositorio nao possuir registos, o utilizador a ser criado será o Administrador
                if (_utilizadorRepositorio.VerificarSeExisteRegisto() == 0)
                {
                    utilizador.UserName = model.Nome;
                    utilizador.CodigoPostal = model.CodigoPostal;
                    utilizador.Email = model.Email;
                    utilizador.PhoneNumber = model.Telefone;
                    utilizador.Foto = model.Foto;
                    utilizador.PrimeiroAcesso = false; // se True = no primeiro login do user, ele terá que redefinir a password
                    utilizador.Status = StatusConta.Aprovado;

                    // obter o Status
                    utilizadorCriado = await _utilizadorRepositorio.CriarUtilizador(utilizador, model.Password);

                    if (utilizadorCriado.Succeeded)
                    {
                        await _utilizadorRepositorio.IncluirUtilizadorEmFuncao(utilizador, "Administrador");
                        await _utilizadorRepositorio.LoginUtilizador(utilizador, false);
                        return RedirectToAction("Index", "Utilizadores"); // UtilizadoresController
                    }
                }

                // se ja houver algum registo faz:

                utilizador.UserName = model.Nome;
                utilizador.CodigoPostal = model.CodigoPostal;
                utilizador.Email = model.Email;
                utilizador.PhoneNumber = model.Telefone;
                utilizador.Foto = model.Foto;
                utilizador.PrimeiroAcesso = true; // se True = no primeiro login do user, ele terá que redifinir a password
                utilizador.Status = StatusConta.Analisando;

                // obter o Status
                utilizadorCriado = await _utilizadorRepositorio.CriarUtilizador(utilizador, model.Password);

                // se o user for criado com sucesso
                if (utilizadorCriado.Succeeded)
                {
                    // segue para a pagina em Analise
                    return View("Analise", utilizador.UserName);
                }

                else
                { // se nao for criado com sucesso, vou percorrer os erros orginados na criaçao do novo user
                    foreach (IdentityError erro in utilizadorCriado.Errors)
                    {
                        ModelState.AddModelError("", erro.Description);
                    }
                    return View(model);
                }


            }
           return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
                await _utilizadorRepositorio.LogoutUtilizador();
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //se os dados forem validos vou procurar pelo user
            if (ModelState.IsValid)
            {
                Utilizador utilizador = await _utilizadorRepositorio.ObterUtilizadorPeloEmail(model.Email);

                if(utilizador != null)
                {

                    if (utilizador.Status == StatusConta.Analisando)
                    {
                        return View("Analise", utilizador.UserName);
                    }

                    else if (utilizador.Status == StatusConta.Reprovado)
                    {
                        return View("Reprovado", utilizador.UserName);
                    }

                    else if (utilizador.PrimeiroAcesso == true)
                    {
                        return RedirectToAction(nameof(RedefinirPassword), utilizador);
                    }

                    else
                    {
                        PasswordHasher<Utilizador> passwordHasher = new PasswordHasher<Utilizador>();

                        // verificar se a password digitada está correta
                        if (passwordHasher.VerifyHashedPassword(utilizador, utilizador.PasswordHash, model.Password) != PasswordVerificationResult.Failed)
                        {
                            await _utilizadorRepositorio.LoginUtilizador(utilizador, false);
                            if (await _utilizadorRepositorio.VerificarSeUtilizadorEstaEmFuncao(utilizador, "Morador"))
                                return RedirectToAction(nameof(MinhasInformacoes));
                            else 
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            ModelState.AddModelError("", "Utilizador e/ou passwords são inválidos");
                            return View(model);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Utilizador e/ou passwords são inválidos");
                    return View(model);
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _utilizadorRepositorio.LogoutUtilizador();
            return RedirectToAction("Login");
        }

        public IActionResult Analise(string nome)
        {
            return View(nome);
        }

        public IActionResult Reprovado(string nome)
        {
            return View(nome);
        }

        public IActionResult AcessoNaoAutorizado()
        {
            return View();
        }

        [Authorize(Roles = "Administrador,Responsavel")]
        public async Task<JsonResult> AprovarUtilizador(string utilizadorId)
        {
            Utilizador utilizador = await _utilizadorRepositorio.ObterPeloId(utilizadorId);
            utilizador.Status = StatusConta.Aprovado;
            await _utilizadorRepositorio.IncluirUtilizadorEmFuncao(utilizador, "Morador");
            await _utilizadorRepositorio.AtualizarUtilizador(utilizador);

            return Json(true);
        }

        [Authorize(Roles = "Administrador,Responsavel")]
        public async Task<JsonResult> ReprovarUtilizador(string utilizadorId)
        {
            Utilizador utilizador = await _utilizadorRepositorio.ObterPeloId(utilizadorId);
            utilizador.Status = StatusConta.Reprovado;
            await _utilizadorRepositorio.AtualizarUtilizador(utilizador);

            return Json(true);
        }


        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> GerirUtilizador(string utilizadorId, string nome)
        {
            if (utilizadorId == null)
                return NotFound();

            TempData["utilizadorId"] = utilizadorId; // para armazenar o ID do utilizador -> TempData é util para armazenar as informaçoes do controller e enviar para a view
            ViewBag.Nome = nome; // para armazenar o nome e ser acedido pela view
            Utilizador utilizador = await _utilizadorRepositorio.ObterPeloId(utilizadorId);

            if (utilizador == null)
                return NotFound();

            List<FuncaoUtilizadorViewModel> viewModel = new List<FuncaoUtilizadorViewModel>();

            foreach (Funcao funcao in await _funcaoRepositorio.ObterTodos())
            {
                FuncaoUtilizadorViewModel model = new FuncaoUtilizadorViewModel
                {
                    FuncaoId = funcao.Id,
                    Nome = funcao.Name,
                    Descricao = funcao.Descricao
                };

                if (await _utilizadorRepositorio.VerificarSeUtilizadorEstaEmFuncao(utilizador, funcao.Name))
                {
                    model.isSelecionado = true;
                }

                else
                    model.isSelecionado = false;

                    viewModel.Add(model); // model é a FuncaoUtilizadorViewModel
            }

            return View(viewModel);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> GerirUtilizador(List<FuncaoUtilizadorViewModel> model)
        {
            string utilizadorId = TempData["utilizadorId"].ToString();

            Utilizador utilizador = await _utilizadorRepositorio.ObterPeloId(utilizadorId);

            if (utilizador == null)
                return NotFound();

            IEnumerable<string> funcoes = await _utilizadorRepositorio.ObterFuncoesUtilizador(utilizador); // obter todas as funcoes do utilizador
            IdentityResult resultado = await _utilizadorRepositorio.RemoverFuncoesUtilizador(utilizador, funcoes);

            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("", "Não foi possível atualizar as funções do utilizador");
                TempData["Exclusao"] = $"Não foi possível atualizar as funções do utilizador {utilizador.UserName}";
                return View("GerirUtilizador", utilizadorId);
            }

            resultado = await _utilizadorRepositorio.IncluirUtilizadorEmFuncoes(utilizador,
                model.Where(x => x.isSelecionado == true).Select(x => x.Nome)); // passei uma lista de strings com os nomes selecionados

            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("", "Não foi possível atualizar as funções do utilizador");
                TempData["Exclusao"] = $"Não foi possível atualizar as funções do utilizador {utilizador.UserName}";
                return View("GerirUtilizador", utilizadorId);
            }

            TempData["Atualizacao"] = $"As funções do utilizador {utilizador.UserName} foram atualizadas";
            return RedirectToAction(nameof(Index));
            
        }

        [Authorize]
        public async Task<IActionResult> MinhasInformacoes()
        {
            return View(await _utilizadorRepositorio.ObterUtilizadorPeloNome(User));
        }

         [Authorize]
        [HttpGet]
        // atualizar user por ID
        public async Task<IActionResult> Atualizar(string id)
        {
            Utilizador utilizador = await _utilizadorRepositorio.ObterPeloId(id);

            if (utilizador == null)
                return NotFound();

            AtualizarViewModel model = new AtualizarViewModel
            {
                UtilizadorId = utilizador.Id,
                Nome = utilizador.UserName,
                CodigoPostal = utilizador.CodigoPostal,
                Email = utilizador.Email,
                Foto = utilizador.Foto,
                Telefone = utilizador.PhoneNumber
            };

            TempData["Foto"] = utilizador.Foto; // gravar a foto em TempData

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(AtualizarViewModel viewModel, IFormFile foto)
        {
            // verificar se os dados sao validos
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorioPasta = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorioPasta, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        viewModel.Foto = "~/Imagens/" + nomeFoto;
                    }
                }

                else
                    viewModel.Foto = TempData["Foto"].ToString();

                Utilizador utilizador = await _utilizadorRepositorio.ObterUtilizadorPeloId(viewModel.UtilizadorId);
                utilizador.UserName = viewModel.Nome;
                utilizador.CodigoPostal = viewModel.CodigoPostal;
                utilizador.PhoneNumber = viewModel.Telefone;
                utilizador.Foto = viewModel.Foto;
                utilizador.Email = viewModel.Email;

                await _utilizadorRepositorio.AtualizarUtilizador(utilizador);

                TempData["Atualizacao"] = "Registo atualizado";

                if (await _utilizadorRepositorio.VerificarSeUtilizadorEstaEmFuncao(utilizador, "Administrador") ||
                    await _utilizadorRepositorio.VerificarSeUtilizadorEstaEmFuncao(utilizador, "Responsavel"))
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                    return RedirectToAction(nameof(MinhasInformacoes));
            }

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult RedefinirPassword(Utilizador utilizador)
        {
            LoginViewModel model = new LoginViewModel
            {
                Email = utilizador.Email
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RedefinirPassword(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Utilizador utilizador = await _utilizadorRepositorio.ObterUtilizadorPeloEmail(model.Email);
                model.Password = _utilizadorRepositorio.EncriptarPassword(utilizador, model.Password);
                utilizador.PasswordHash = model.Password;
                utilizador.PrimeiroAcesso = false;
                await _utilizadorRepositorio.AtualizarUtilizador(utilizador);
                await _utilizadorRepositorio.LoginUtilizador(utilizador, false);

                return RedirectToAction(nameof(MinhasInformacoes));
            }

            return View(model);
        }

    }
}
