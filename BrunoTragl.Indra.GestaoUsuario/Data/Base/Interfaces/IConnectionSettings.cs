using System.Data.SqlClient;

namespace BrunoTragl.Indra.GestaoUsuario.Data.Base.Interfaces
{
    public interface IConnectionSettings
    {
        string ConnectionString { get; set; }
        int CommandTimeout { get; set; }
        SqlConnection Connection { get; set; }
        void CloseConnection();
    }
}
