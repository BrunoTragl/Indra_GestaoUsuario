using BrunoTragl.Indra.GestaoUsuario.Models;
using System.Collections.Generic;

namespace BrunoTragl.Indra.GestaoUsuario.Data.Interfaces
{
    public interface IUsuarioData
    {
        void AddRange(IEnumerable<UsuarioModel> usuarios);
        IList<UsuarioModel> List();
        void Edit(UsuarioModel usuario);
    }
}
