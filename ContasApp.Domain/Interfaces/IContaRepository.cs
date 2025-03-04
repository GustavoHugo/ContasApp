using ContasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Domain.Interfaces
{
    public interface IContaRepository:IRepository<Conta>
    {
       IEnumerable<Conta> ObterPorFiltro(ContaFiltro filtro);
    }
}
