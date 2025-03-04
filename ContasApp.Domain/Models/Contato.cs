using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Domain.Models
{
    public class Contato
    {
        public string UsuarioId { get; set; }
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public PessoaFisicaJuridica Tipo { get; set; }

    }
}
