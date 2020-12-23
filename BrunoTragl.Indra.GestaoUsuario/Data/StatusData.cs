using BrunoTragl.Indra.GestaoUsuario.Data.Base;
using BrunoTragl.Indra.GestaoUsuario.Data.Base.Interfaces;
using BrunoTragl.Indra.GestaoUsuario.Data.Enumerables;
using BrunoTragl.Indra.GestaoUsuario.Data.Interfaces;
using BrunoTragl.Indra.GestaoUsuario.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BrunoTragl.Indra.GestaoUsuario.Data
{
    public class StatusData : SqlConfiguration, IStatusData
    {
        public StatusData(IConnectionSettings connectionSettings)
            : base(connectionSettings)
        { }

        public void AddRange(IEnumerable<StatusModel> manyStatus)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                foreach (StatusModel status in manyStatus)
                {
                    parameters.Clear();
                    parameters.Add("@id", status.Id);
                    parameters.Add("@nome", status.Nome);
                    SqlDataReader sqlDataReader = ExecuteStoredProcedure(StatusStoredProcedures.proc_incluir_status.ToString(), parameters);
                    int erro;
                    if (!ValidateStoredProcedure(sqlDataReader, UsuarioStoredProcedures.proc_incluir_usuario.ToString(), out erro))
                    {
                        if (erro == -2)
                        {
                            Console.WriteLine($"Status {status.Nome} já consta cadastrado.");
                        }
                        else
                        {
                            throw new Exception($"Ocorreu o erro '{erro}' ao inserir status com id {status.Id} e nome {status.Nome}.");
                        }
                    }
                    sqlDataReader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<StatusModel> List()
        {
            IList<StatusModel> list = new List<StatusModel>();
            SqlDataReader sqlDataReader = ExecuteStoredProcedure(StatusStoredProcedures.proc_listar_status.ToString());
            while (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    list.Add(new StatusModel
                    {
                        Id = int.Parse(sqlDataReader["id"].ToString()),
                        Nome = sqlDataReader["nome"].ToString(),
                        Criacao = DateTime.Parse(sqlDataReader["criacao"].ToString())
                    });
                }
                sqlDataReader.NextResult();
            }
            sqlDataReader.Close();
            return list;
        }
    }
}
