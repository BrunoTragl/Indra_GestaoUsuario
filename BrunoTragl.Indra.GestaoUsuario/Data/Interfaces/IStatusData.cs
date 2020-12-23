using BrunoTragl.Indra.GestaoUsuario.Models;
using System.Collections.Generic;

namespace BrunoTragl.Indra.GestaoUsuario.Data.Interfaces
{
    public interface IStatusData
    {
        void AddRange(IEnumerable<StatusModel> manyStatus);
        IList<StatusModel> List();
    }
}
