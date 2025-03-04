using ContaApp.UI.Web.Models;
using ContasApp.Domain.Interfaces;
using ContasApp.Domain.Models;
using ContasApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;

namespace ContaApp.UI.Web.Controllers
{
    public class AppController : Controller
    {

        public ActionResult Registro()
        {
            var registro = new RegistroViewModel();

            return View(registro);
        }

        [HttpPost]

        public ActionResult Registro(RegistroViewModel registro)
        {
            if (string.IsNullOrEmpty(registro.Email)) 
            {
                ModelState.AddModelError("Email", "O email deve ser informado");
            }

            if (string.IsNullOrEmpty(registro.Senha))
            {
                ModelState.AddModelError("Senha", "A senha deve ser informada");
            }
            else 
            { 
                if (registro.Senha != registro.ConfirmarSenha)
                {
                    ModelState.AddModelError("ConfirmarSenha", "As senhas não coincidem");
                }
            }

            if (ModelState.IsValid) 
            {
                var usuario = new Usuario();
                usuario.Email = registro.Email;
                usuario.Senha = registro.Senha;
                usuario.Nome = registro.Nome;
                usuario.Id = Guid.NewGuid().ToString();

                var usuarioRepo = AppHelper.ObterUsuarioRepository();
                usuarioRepo.Incluir(usuario);

                AppHelper.RegistrarUsuario(usuario);

                return RedirectToAction("Inicio");
            }

            return View(registro);
        }


        public ActionResult Login() 
        {
            var login = new LoginViewModel();

            return View(login);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            IUsuarioRepository rep = AppHelper.ObterUsuarioRepository();

            Usuario usuario = rep.ObterPorEmailSenha(login.Email, login.Senha);
            if (usuario == null)
            {
                login.Mensagem = "Usuário ou senha inexistente";
            }
            else 
            {
                AppHelper.RegistrarUsuario(usuario);

                return RedirectToAction("Inicio");
            }

                return View(login);
        }

        public ActionResult Inicio()
        {
            var usuario = AppHelper.ObterUsuarioLogado();
            if (usuario == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult Sobre()
        {
            return View();
        }
    }
}