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
    public class UsuarioData : SqlConfiguration, IUsuarioData
    {
        public UsuarioData(IConnectionSettings connectionSettings)
            : base(connectionSettings)
        { }

        public void AddRange(IEnumerable<UsuarioModel> usuarios)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                foreach (UsuarioModel usuario in usuarios)
                {
                    parameters.Clear();
                    parameters.Add("@nome", usuario.Nome);
                    parameters.Add("@data_nascimento", usuario.DataNascimento);
                    parameters.Add("@id_perfil_usuario", usuario.PerfilUsuario.Id);
                    SqlDataReader sqlDataReader = ExecuteStoredProcedure(UsuarioStoredProcedures.proc_incluir_usuario.ToString(), parameters);
                    int erro;
                    if (!ValidateStoredProcedure(sqlDataReader, UsuarioStoredProcedures.proc_incluir_usuario.ToString(), out erro))
                    {
                        if (erro == -2)
                        {
                            Console.WriteLine($"Usuario com nome {usuario.Nome} já consta cadastrado.");
                        }
                        else
                        {
                            throw new Exception($"Ocorreu o erro '{erro}' ao inserir usuario com nome {usuario.Nome}, data de nascimento {usuario.DataNascimento} e id perfil usuário {usuario.PerfilUsuario.Id}.");
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

        public IList<UsuarioModel> List()
        {
            try
            {
                List<UsuarioModel> usuarios = new List<UsuarioModel>();
                SqlDataReader sqlDataReader = ExecuteStoredProcedure(UsuarioStoredProcedures.proc_listar_usuarios.ToString());
                while (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        usuarios.Add(new UsuarioModel
                        {
                            Id = sqlDataReader.GetInt32(0),
                            Nome = sqlDataReader.GetString(1),
                            DataNascimento = sqlDataReader.GetDateTime(2),
                            PerfilUsuario = new PerfilUsuarioModel
                            {
                                Id = sqlDataReader.GetInt32(3)
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

        public void Edit(UsuarioModel usuario)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@id", usuario.Id);
                parameters.Add("@nome", usuario.Nome);
                parameters.Add("@data_nascimento", usuario.DataNascimento);
                parameters.Add("@id_perfil_usuario", usuario.PerfilUsuario.Id);
                SqlDataReader sqlDataReader = ExecuteStoredProcedure(UsuarioStoredProcedures.proc_alterar_usuario.ToString(), parameters);
                int erro;
                if (!ValidateStoredProcedure(sqlDataReader, UsuarioStoredProcedures.proc_incluir_usuario.ToString(), out erro))
                {
                    if (erro == -1)
                    {
                        Console.WriteLine($"Não existe nenhum registro de Usuário com o id {usuario.Id}.");
                    }
                    else
                    {
                        throw new Exception($"Ocorreu o erro '{erro}' ao tentar alterar usuário com nome {usuario.Nome}, data de nascimento {usuario.DataNascimento} e id perfil usuário {usuario.PerfilUsuario.Id}.");
                    }
                }
                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
