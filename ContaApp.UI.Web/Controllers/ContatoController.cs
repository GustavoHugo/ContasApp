﻿using ContaApp.UI.Web.Models;
using ContasApp.Domain.Interfaces;
using ContasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContaApp.UI.Web.Controllers
{
    public class ContatoController : Controller
    {
        private IContatoRepository repositorio;
        private Usuario usuario;

        public ContatoController()
        {
            repositorio = AppHelper.ObterContatoRepository();
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
            var contato = new ContatoViewModel();
            return View(contato);
        }

        [HttpPost]
        public ActionResult Incluir(ContatoViewModel contatoViewModel)
        {
            if (string.IsNullOrEmpty(contatoViewModel.Nome))
            {
                ModelState.AddModelError("Nome", "O nome deve ser informado");
            }
            if (ModelState.IsValid) 
            {
                Contato contato;
                if (contatoViewModel.Tipo == PessoaFisicaJuridica.PessoaFisica)
                {
                    contato = new Pessoa();
                    ((Pessoa)contato).RG=contatoViewModel.RG;
                    ((Pessoa)contato).CPF = contatoViewModel.CPF;
                    ((Pessoa)contato).DataNascimento = contatoViewModel.DataNascimento;
                    contato.Tipo = PessoaFisicaJuridica.PessoaFisica;
                }
                else 
                {
                    contato = new Empresa();
                    ((Empresa)contato).CNPJ = contatoViewModel.CNPJ;
                    contato.Tipo = PessoaFisicaJuridica.PessoaJuridica;
                }
                contato.UsuarioId=usuario.Id;
                contato.Id = Guid.NewGuid().ToString();
                contato.Nome = contatoViewModel.Nome;
                contato.Telefone = contatoViewModel.Telefone;
                contato.Email = contatoViewModel.Email;

                repositorio.Incluir(contato);
                return RedirectToAction("Inicio");   
            }

            return View(contatoViewModel);
        }

        public ActionResult Alterar(string id) 
        {
            var contato = repositorio.ObterPorId(id);
            var contatoViewModel = new ContatoViewModel()
            {
                Email = contato.Email,
                Nome = contato.Nome,
                Id = contato.Id,
                Telefone = contato.Telefone
            };
            if (contato is Empresa)
            {
                contatoViewModel.CNPJ = ((Empresa)contato).CNPJ;
            }
            else 
            {
                contatoViewModel.CPF = ((Pessoa)contato).CPF;
                contatoViewModel.RG = ((Pessoa)contato).RG;
                contatoViewModel.DataNascimento = ((Pessoa)contato).DataNascimento;
            }

            return View(contatoViewModel);
        }

        [HttpPost]
        public ActionResult Alterar(ContatoViewModel contatoViewModel)
        {
            if (string.IsNullOrEmpty(contatoViewModel.Nome))
            {
                ModelState.AddModelError("Nome", "O nome deve ser informado");
            }
            if (ModelState.IsValid)
            {
                Contato contato;
                if (contatoViewModel.Tipo == PessoaFisicaJuridica.PessoaFisica)
                {
                    contato = new Pessoa();
                    ((Pessoa)contato).RG = contatoViewModel.RG;
                    ((Pessoa)contato).CPF = contatoViewModel.CPF;
                    ((Pessoa)contato).DataNascimento = contatoViewModel.DataNascimento;
                    contato.Tipo = PessoaFisicaJuridica.PessoaFisica;
                }
                else
                {
                    contato = new Empresa();
                    ((Empresa)contato).CNPJ = contatoViewModel.CNPJ;
                    contato.Tipo = PessoaFisicaJuridica.PessoaJuridica;
                }
                contato.UsuarioId = usuario.Id;
                contato.Id = contatoViewModel.Id;
                contato.Nome = contatoViewModel.Nome;
                contato.Telefone = contatoViewModel.Telefone;
                contato.Email = contatoViewModel.Email;

                repositorio.Alterar(contato);
                return RedirectToAction("Inicio");
            }

            return View(contatoViewModel);
        }

        public ActionResult Excluir(string id) 
        {

            var contato = repositorio.ObterPorId(id);
            var contatoViewModel = new ContatoViewModel()
            {
                Email = contato.Email,
                Nome = contato.Nome,
                Id = contato.Id,
                Telefone = contato.Telefone
            };
            if (contato is Empresa)
            {
                contatoViewModel.CNPJ = ((Empresa)contato).CNPJ;
                contatoViewModel.Tipo = PessoaFisicaJuridica.PessoaJuridica;
            }
            else
            {
                contatoViewModel.CPF = ((Pessoa)contato).CPF;
                contatoViewModel.RG = ((Pessoa)contato).RG;
                contatoViewModel.DataNascimento = ((Pessoa)contato).DataNascimento;
                contatoViewModel.Tipo = PessoaFisicaJuridica.PessoaFisica;
            }

            return View(contatoViewModel);
        }

        [HttpPost]
        public ActionResult Excluir(string id, FormCollection form)
        {
            repositorio.Excluir(id);
            return RedirectToAction("Inicio");
        }


    }


}