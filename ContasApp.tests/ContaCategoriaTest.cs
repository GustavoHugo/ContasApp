using System;
using ContasApp.Domain.Models;
using ContasApp.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContasApp.tests
{
	[TestClass]
	public class ContaCategoriaTest
	{
        ContaCategoriaRepository rep = new ContaCategoriaRepository();

        [TestMethod]
		public void IncluirTest()
		{
			var conta = new ContaCategoria()
			{
				Id = "12345",
				Nome = "PauloJr",
				UsuarioId = "54321"
			};
			rep.Incluir(conta);
		}


        [TestMethod]
        public void AlterarTest()
        {
            var conta = new ContaCategoria()
            {
                Id = "12345",
                Nome = "PauloJrAlterado",
                UsuarioId = "54321"
            };
            rep.Alterar(conta);
        }


        [TestMethod]
        public void ExcluirTest()
        {
           rep.Excluir("123");
        }


        [TestMethod]
        public void ObterTodosTest()
        {
            var lista =  rep.ObterTodos("54321");
            foreach (var conta in lista)
            {
                Exibir(conta);
            }
        }
        private void Exibir(ContaCategoria conta)
        {
            Console.WriteLine(conta.Id);
            Console.WriteLine(conta.UsuarioId);
            Console.WriteLine(conta.Nome);
        }


        [TestMethod]
        public void ObterPorIdTest()
        {
            var conta = rep.ObterPorId("12345");
            if (conta != null) 
            {
                Exibir(conta);
            }
        }
    }
}
