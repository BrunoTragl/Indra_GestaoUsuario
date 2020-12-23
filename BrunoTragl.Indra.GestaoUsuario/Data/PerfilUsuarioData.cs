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
    public class PerfilUsuarioData : SqlConfiguration, IPerfilUsuarioData
    {
        public PerfilUsuarioData(IConnectionSettings connectionSettings)
            : base(connectionSettings)
        { }

        public void AddRange(IEnumerable<PerfilUsuarioModel> perfis)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                foreach (PerfilUsuarioModel perfil in perfis)
                {
                    parameters.Clear();
                    parameters.Add("@id", perfil.Id);
                    parameters.Add("@nome", perfil.Nome);
                    parameters.Add("@id_status", perfil.Status.Id);
                    SqlDataReader sqlDataReader = ExecuteStoredProcedure(PerfilUsuarioStoredProcedures.proc_incluir_perfil_usuario.ToString(), parameters);
                    int erro;
                    if (!ValidateStoredProcedure(sqlDataReader, PerfilUsuarioStoredProcedures.proc_incluir_perfil_usuario.ToString(), out erro))
                    {
                        if (erro == -2)
                        {
                            Console.WriteLine($"Perfil {perfil.Nome} já consta cadastrado.");
                        }
                        else
                        {
                            throw new Exception($"Ocorreu o erro '{erro}' ao inserir perfil com id {perfil.Id}, nome {perfil.Nome} e id_status {perfil.Status.Id}.");
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

        public IList<PerfilUsuarioModel> List()
        {
            try
            {
                List<PerfilUsuarioModel> usuarios = new List<PerfilUsuarioModel>();
                SqlDataReader sqlDataReader = ExecuteStoredProcedure(PerfilUsuarioStoredProcedures.proc_listar_perfil_usuario.ToString());
                while (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        usuarios.Add(new PerfilUsuarioModel
                        {
                            Id = sqlDataReader.GetInt32(0),
                            Nome = sqlDataReader.GetString(1),
                            Status = new StatusModel
                            {
                                Id = sqlDataReader.GetInt32(2)
                            }
                        });
                    }
                    sqlDataReader.NextResult();
                }
                sqlDataReader.Close();
                return usuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
