using ContasApp.Domain.Interfaces;
using ContasApp.Domain.Models;
using ContasApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ContaApp.UI.Web.Controllers
{
    public class ContaCorrenteController : Controller
    {

        private IContaCorrenteRepository repositorio;
        private Usuario usuario;

        public ContaCorrenteController() 
        {
            repositorio = AppHelper.ObterContaCorrenteRepository();
            usuario = AppHelper.ObterUsuarioLogado();
        }

        public ActionResult Inicio()
        {
            if (usuario == null) return RedirectToAction("Login", "App");
            var lista = repositorio.ObterTodos(usuario.Id);
            return View(lista);
        }

        public ActionResult Incluir()
        {
            var contaCorrente = new ContaCorrente();
            return View(contaCorrente);
        }

        [HttpPost]
        public ActionResult Incluir(ContaCorrente contaCorrente)
        {
            if (string.IsNullOrEmpty(contaCorrente.Descricao)) 
            {
                ModelState.AddModelError("Descrição", "A descrição precisa ser preenchida");
            }
            if (ModelState.IsValid) 
            {
                contaCorrente.Id = Guid.NewGuid().ToString();
                contaCorrente.UsuarioId = usuario.Id;
                repositorio.Incluir(contaCorrente);
                return RedirectToAction("Inicio");
            }
            return View(contaCorrente);
        }
    }
}