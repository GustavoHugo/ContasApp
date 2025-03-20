using ContaApp.UI.Web.Models;
using ContasApp.Domain.Interfaces;
using ContasApp.Domain.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContaApp.UI.Web.Controllers
{
    public class ContaController : Controller
    {
        private void PreencherViewModel(ContaViewModel viewModel)
        {
            var contaCorrenteRep = AppHelper.ObterContaCorrenteRepository();
            viewModel.ContaCorrenteList = contaCorrenteRep.ObterTodos(usuario.Id).ToList();

            var contaCategoriaRep = AppHelper.ObterContaCategoriaRepository();
            viewModel.ContaCategoriaList = contaCategoriaRep.ObterTodos(usuario.Id).ToList();

            var contatoRep = AppHelper.ObterContatoRepository();
            viewModel.ContatoList = contatoRep.ObterTodos(usuario.Id).ToList();
        }
        private IContaRepository repositorio;
        private Usuario usuario;

        public ContaController()
        {
            repositorio = AppHelper.ObterContaRepository();
            usuario = AppHelper.ObterUsuarioLogado();
        }

        public ActionResult Inicio()
        {
            if (usuario == null) return RedirectToAction("Login", "App");
            var lista = repositorio.ObterPorUsuario(usuario.Id);
            return View(lista);
        }

        public ActionResult Incluir()
        {
            if (usuario == null) return RedirectToAction("Login", "App");
            var viewModel = new ContaViewModel();

            PreencherViewModel(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Incluir(ContaViewModel viewModel)
        {
            if (usuario == null) return RedirectToAction("Login", "App");
            try
            {
                viewModel.ContaInstancia.UsuarioId = usuario.Id;
                viewModel.ContaInstancia.Id = Guid.NewGuid().ToString();
                repositorio.Incluir(viewModel.ContaInstancia);
                return RedirectToAction("Inicio");
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", ex.Message);
            }
            PreencherViewModel(viewModel);
            return View(viewModel); 
        }

        public ActionResult Alterar(string id)
        {
            if (usuario == null) return RedirectToAction("Login", "App");
            var viewModel = new ContaViewModel();

            viewModel.ContaInstancia = repositorio.ObterPorId(id);
            PreencherViewModel(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Alterar(ContaViewModel viewModel)
        {
            if (usuario == null) return RedirectToAction("Login", "App");
            try
            {
                viewModel.ContaInstancia.UsuarioId = usuario.Id;
                repositorio.Alterar(viewModel.ContaInstancia);
                return RedirectToAction("Inicio");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            PreencherViewModel(viewModel);
            return View(viewModel);
        }

        public ActionResult Excluir(string id)
        {
            if (usuario == null) return RedirectToAction("Login", "App");
            var conta = repositorio.ObterExibirPorId(id);
            return View(conta);
        }

        [HttpPost]
        public ActionResult Excluir(string id, FormCollection form)
        {
            repositorio.Excluir(id);
            return RedirectToAction("Inicio");
        }
    }
}