using ContasApp.Domain.Interfaces;
using ContasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        public void Alterar(Contato contato)
        {
            Db.Execute("ContatoAlterar", contato);
        }

        public void Excluir(string id)
        {
            Db.Execute("ContatoExcluir", new { Id=@id });
        }

        public void Incluir(Contato contato)
        {
            if (contato is Empresa) 
            {
                Db.Execute("ContatoEmpresaIncluir", contato);
            }
            else 
            { 
                Db.Execute("ContatoPessoaIncluir", contato);
            }
        }

        public Contato ObterPorId(string id)
        {
            var contato = Db.QueryEntidade<Contato>("ContatoObterPorId", new {Id=@id});

            if (contato.Tipo == PessoaFisicaJuridica.PessoaJuridica) 
            {
                var empresa = Db.QueryEntidade<Empresa>("ContatoObterPorId", new { Id = @id });
                return empresa;
            }
            else
            {
                var pessoa = Db.QueryEntidade<Pessoa>("ContatoObterPorId", new { Id = @id });
                return pessoa;
            }

        }

        public IEnumerable<Contato> ObterTodos(string usuarioId)
        {
            return Db.QueryColecao<Contato>("ContatoObterTodos", new {UsuarioId=@usuarioId});
        }

        public IEnumerable<string> Validar()
        {
            throw new NotImplementedException();
        }
    }
}
