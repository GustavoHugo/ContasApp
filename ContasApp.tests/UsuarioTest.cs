using System;
using ContasApp.Domain.Models;
using ContasApp.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Net.Mime.MediaTypeNames;

namespace ContasApp.Tests
{
	[TestClass]
	public class Usuariotest
	{
		[TestMethod]
		public void UsuarioObterTodosTest()
		{
			var rep = new UsuarioRepository();
			var lista = rep.ObterTodos(null);
			foreach(var usuario in lista)
			{
				Console.WriteLine(usuario.Nome);
                Console.WriteLine(usuario.Email);
            }

		}

        [TestMethod]
        public void UsuarioIncluirTest()
        {
            var usuario = new Usuario() 
            { 
                Id= "123teste", 
                Nome = "Teste", 
                Email = "teste@teste.com", 
                Senha = "teste123"
            };
            var rep = new UsuarioRepository();
            rep.Incluir(usuario);

        }

        [TestMethod]
        public void UsuarioAlterarTest()
        {
            var usuario = new Usuario()
            {
                Id = "123teste",
                Nome = "Teste alteracao",
                Email = "teste1@teste.com",
                Senha = "teste1233"
            };
            var rep = new UsuarioRepository();
            rep.Alterar(usuario);
        }

        [TestMethod]
        public void UsuarioExcluirTest()
        {
           
            var rep = new UsuarioRepository();
            rep.Excluir("123teste");
        }

        [TestMethod]
        public void UsuarioObterPorIdTest()
        {
          
            var rep = new UsuarioRepository();
            var usuario = rep.ObterPorId("123teste");

            if (usuario != null)
            {
                Console.WriteLine(usuario.Id);
                Console.WriteLine(usuario.Email);
                Console.WriteLine(usuario.Senha);
                Console.WriteLine(usuario.Nome);
            }

        }

        [TestMethod]
        public void UsuarioObterPorEmailSenhaTest()
        {

            var rep = new UsuarioRepository();
            var usuario = rep.ObterPorEmailSenha("teste@teste.com", "teste123");

            if (usuario != null)
            {
                Console.WriteLine(usuario.Id);
                Console.WriteLine(usuario.Email);
                Console.WriteLine(usuario.Senha);
                Console.WriteLine(usuario.Nome);
            }

        }
    }
}
