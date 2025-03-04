using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Domain.Models
{
    public class ContaFiltro
    {
        public string UsuarioId { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public PagarReceber? Tipo { get; set; }
        public string ContaCategoriaId {get;set;}
        public string ContatoId { get; set; }

    }
}
