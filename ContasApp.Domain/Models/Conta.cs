using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Domain.Models
{
    public class Conta
    {
        public string Id { get; set; }
        public string UsuarioId { get; set; }
        public string ContaCorrenteId { get; set; }
        public PagarReceber Tipo { get; set; }
        public string ContaCategoriaId { get; set; }
        public string ContatoId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Acrescimo { get; set; }
        public decimal? ValorPago { get; set; }

    }
}
