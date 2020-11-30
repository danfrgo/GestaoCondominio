using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Interface;
using GestaoCondominios.ViewModels;
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
        private readonly IWebHostEnvironment _webHostEnvironment;// para conseguir gravar a foto num diretorio

        public UtilizadoresController(IUtilizadorRepositorio utilizadorRepositorio, IWebHostEnvironment webHostEnvironment)
        {
            _utilizadorRepositorio = utilizadorRepositorio;
            _webHostEnvironment = webHostEnvironment; // as imagens vao ficar no diretorio imagens
        }


        public async Task<IActionResult> Index()
        {
            return  View(await _utilizadorRepositorio.ObterTodos());
        }

        [HttpGet]
        public IActionResult Registo()
        {
            return View();
        }

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
                    utilizador.PrimeiroAcesso = false; // se True = no primeiro login do user, ele terá que redifinir a password
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

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
                await _utilizadorRepositorio.LogoutUtilizador();
            return View();
        }

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
                        return View("RedifinirPassword", utilizador);
                    }

                    else
                    {
                        PasswordHasher<Utilizador> passwordHasher = new PasswordHasher<Utilizador>();

                        // verificar se a password digitada está correta
                        if (passwordHasher.VerifyHashedPassword(utilizador, utilizador.PasswordHash, model.Password) != PasswordVerificationResult.Failed)
                        {
                            await _utilizadorRepositorio.LoginUtilizador(utilizador, false);
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

    }
}
