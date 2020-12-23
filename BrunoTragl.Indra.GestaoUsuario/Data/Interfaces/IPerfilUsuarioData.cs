using BrunoTragl.Indra.GestaoUsuario.Models;
using System.Collections.Generic;

namespace BrunoTragl.Indra.GestaoUsuario.Data.Interfaces
{
    public interface IPerfilUsuarioData
    {
        void AddRange(IEnumerable<PerfilUsuarioModel> perfis);
        IList<PerfilUsuarioModel> List();
    }
}
