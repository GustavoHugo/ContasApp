using ContasApp.Domain.Interfaces;
using ContasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContaApp.UI.Web.Controllers
{
    public class ContaCategoriaController : Controller
    {

        private IContaCategoriaRepository repositorio;
        private Usuario usuario;

        public ContaCategoriaController() 
        {
            repositorio = AppHelper.ObterContaCategoriaRepository();
            usuario = AppHelper.ObterUsuarioLogado();
        }

        public ActionResult Inicio()
        {
            if (usuario == null) return RedirectToAction("Login", "App");
            var lista = repositorio.ObterTodos(usuario.Id);
            return View(lista);
        }

        public ActionResult Excluir(string id)
        {
            var contaCategoria = repositorio.ObterPorId(id);
            return View(contaCategoria);
        }

        [HttpPost]
        public ActionResult Excluir(string id, FormCollection form)
        {
            repositorio.Excluir(id);
            return RedirectToAction("Inicio");
        }


        public ActionResult Alterar(string id)
        {
            var contaCategoria = repositorio.ObterPorId(id);
            return View(contaCategoria);
        }

        [HttpPost]
        public ActionResult Alterar(ContaCategoria contaCategoria)
        {
            if (string.IsNullOrEmpty(contaCategoria.Nome))
            {
                ModelState.AddModelError("Nome", "O Nome precisa ser preenchido");
            }

            if (ModelState.IsValid)
            {
                repositorio.Alterar(contaCategoria);
                return RedirectToAction("Inicio");
            }
            return View(contaCategoria);

        }


        public ActionResult Incluir()
        {
            var contaCorrente = new ContaCategoria();
            return View(contaCorrente);
        }

        [HttpPost]
        public ActionResult Incluir(ContaCategoria contaCategoria)
        {
            if (string.IsNullOrEmpty(contaCategoria.Nome))
            {
                ModelState.AddModelError("Nome", "O Nome precisa ser preenchido");
            }
            if (ModelState.IsValid)
            {
                contaCategoria.Id = Guid.NewGuid().ToString();
                contaCategoria.UsuarioId = usuario.Id;
                repositorio.Incluir(contaCategoria);
                return RedirectToAction("Inicio");
            }
            return View(contaCategoria);
        }

    }
}