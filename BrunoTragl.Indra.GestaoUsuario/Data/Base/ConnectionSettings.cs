using BrunoTragl.Indra.GestaoUsuario.Data.Base.Interfaces;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace BrunoTragl.Indra.GestaoUsuario.Data.Base
{
    public class ConnectionSettings : IConnectionSettings
    {
        public string ConnectionString { get; set; }
        public int CommandTimeout { get; set; }
        public SqlConnection Connection { get; set; }

        public ConnectionSettings()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["GestaoUsuarioConnection"].ConnectionString;
            if (int.TryParse(ConfigurationManager.AppSettings["CommandTimeout"], out int timeout))
                this.CommandTimeout = timeout;
            this.Connection = new SqlConnection(this.ConnectionString);
            this.OpenConnection();
        }

        public void OpenConnection()
        {
            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CloseConnection()
        {
            try
            {
                Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
