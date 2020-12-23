using BrunoTragl.Indra.GestaoUsuario.Data.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BrunoTragl.Indra.GestaoUsuario.Data.Base
{
    public abstract class SqlConfiguration
    {
        protected SqlConnection _connection;
        private int _commandTimeout;
        private IConnectionSettings _connectionSettings;
        protected SqlConfiguration(IConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
            _connection = connectionSettings.Connection;
            _commandTimeout = connectionSettings.CommandTimeout;
        }
        
        protected SqlDataReader ExecuteStoredProcedure(string command, Dictionary<string, object> parameters = null)
        {
            try
            {
                SqlCommand sqlCommand = _connection.CreateCommand();
                sqlCommand.CommandText = command;
                sqlCommand.CommandTimeout = _commandTimeout;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SetParameters(sqlCommand, parameters);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool ValidateStoredProcedure(SqlDataReader sqlDataReader, string storeProcedureName, out int retorno)
        {
            try
            {
                retorno = 0;
                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();
                    retorno = int.Parse(sqlDataReader[0].ToString());
                    if (retorno > 0)
                        throw new Exception($"Ocorreu um erro ao executar a StoredProcedure {storeProcedureName} com retorno {retorno}");
                    else if (retorno < 0)
                        return false;
                    else
                        return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _connectionSettings.CloseConnection();
                throw ex;
            }
        }

        private void SetParameters(SqlCommand sqlCommand, Dictionary<string, object> parameters)
        {
            if (parameters != null && parameters.Count > 0)
                foreach (var parameter in parameters)
                    sqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
        }
    }
}
