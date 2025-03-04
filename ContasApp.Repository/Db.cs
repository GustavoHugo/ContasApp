using ContasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections;

namespace ContasApp.Repository
{
    public static class Db
    {
        private static string _conexao= "server=DESKTOP-F4L3UFV; database=contadb; user=sa; password=Summonerdexter@7";

        private static SqlConnection ObterConnection()
        {
            var cn = new SqlConnection();
            cn.ConnectionString = _conexao;
            return cn;
        }

        public static int Execute(string storeProcedure, object param)
        {
            int total;
            using (var cn = ObterConnection())
            {
                total = cn.Execute(storeProcedure, param, commandType: CommandType.StoredProcedure);
            }
            return total;
        }

        public static IEnumerable<T> QueryColecao<T>(string storeProcedure, object param)
        {
            IEnumerable<T> retorno;
            using (var cn = ObterConnection())
            {
                retorno = cn.Query<T>(storeProcedure, param, commandType: CommandType.StoredProcedure);
            }
            return retorno;
        }
        
        public static T QueryEntidade<T>(string storeProcedure, object param)
        {
            T retorno;
            using (var cn = ObterConnection())
            {
                retorno = cn.QueryFirstOrDefault<T>(storeProcedure, param, commandType:CommandType.StoredProcedure);
            }
            return retorno;
        }
    }
}
