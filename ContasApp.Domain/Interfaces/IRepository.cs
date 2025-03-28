﻿using ContasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Domain.Interfaces
{
    public interface IRepository<T>
    {
        void Incluir(T entidade);
        void Alterar(T entidade);
        void Excluir(string id);
        T ObterPorId(string id);
        IEnumerable<T> ObterTodos(string usuarioId);
        IEnumerable<string> Validar();

    }
}
