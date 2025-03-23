using ContasApp.Domain.Interfaces;
using ContasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Repository
{
    public class ContaRepository : IContaRepository
    {
        public void Alterar(Conta conta)
        {
            Db.Execute("ContaAlterar", conta);
        }

        public ContaExibirViewModel ObterExibirPorId(string id)
        {
            return Db.QueryEntidade<ContaExibirViewModel>("ContaObterExibirPorId", new { Id = @id });
        }

        public void Excluir(string id)
        {
            Db.Execute("ContaExcluir", new { Id = @id });
        }

        public void Incluir(Conta conta)
        {
            Db.Execute("ContaIncluir", conta);
        }

        public Conta ObterPorId(string id)
        {
            return Db.QueryEntidade<Conta>("ContaObterPorId", new { Id = id });
        }

        public IEnumerable<ContaListItem> ObterPorUsuario(string usuarioId)
        {
            return Db.QueryColecao<ContaListItem>("ContaObterTodos", new { UsuarioId = @usuarioId });
        }

        public IEnumerable<Conta> ObterTodos(string usuarioId)
        {
            return Db.QueryColecao<Conta>("ContaObterTodos", usuarioId);
        }

        public IEnumerable<string> Validar()
        {
            throw new NotImplementedException();
        }

        IEnumerable<ContaListItem> ObterPorFiltro(ContaFiltro filtro)
        {
            if (filtro.DataInicial == DateTime.MinValue) filtro.DataInicial = new DateTime(1753,1,1);
            if (filtro.DataFinal == DateTime.MinValue) filtro.DataFinal = new DateTime(9999,12,31);

            var lista = Db.QueryColecao<ContaListItem>("ContaObterEntreDatas",
                new
                {
                    DataInicial = filtro.DataInicial,
                    DataFinal = filtro.DataFinal,
                    UsuarioId = filtro.UsuarioId,
                }).ToList();

            var listaFiltrada = lista.ToList();

            if (filtro.ContaCorrenteId != null)
            {
                listaFiltrada = listaFiltrada.ToList().Where(m => m.ContaCorrenteId == filtro.ContaCorrenteId).ToList();
            }
            if(filtro.ContaCategoriaId != null)
            {
                listaFiltrada = listaFiltrada.ToList().Where(m => m.ContaCategoriaId == filtro.ContaCategoriaId).ToList();
            }

            return listaFiltrada;
        }

        IEnumerable<ContaListItem> IContaRepository.ObterPorFiltro(ContaFiltro filtro)
        {
            return ObterPorFiltro(filtro);
        }
    }
}
