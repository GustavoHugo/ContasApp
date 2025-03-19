﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Domain.Models
{
    public class ContaListItem
    {
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public PagarReceber Tipo {  get; set; } 
        public string Descricao { get; set; }
        public string Contato { get; set; } 
        public string Categoria { get; set; }   
        public string Valor {  get; set; }  


    }
}
