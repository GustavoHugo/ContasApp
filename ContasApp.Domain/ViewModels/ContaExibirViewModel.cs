﻿using ContasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Domain.Models
{
    public class ContaExibirViewModel:Conta
    {
        public string ContaCorrenteDescricao { get; set; }
        public string CategoriaNome { get; set; }   
        public string ContatoNome { get; set; }

    }
}
