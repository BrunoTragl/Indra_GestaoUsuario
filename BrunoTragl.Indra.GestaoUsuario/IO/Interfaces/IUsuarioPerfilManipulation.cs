using BrunoTragl.Indra.GestaoUsuario.DTO;
using System.Collections.Generic;

namespace BrunoTragl.Indra.GestaoUsuario.IO.Interfaces
{
    public interface IUsuarioPerfilManipulation
    {
        IList<UsuarioPerfilDto> GetUsuarioPerfilData();
    }
}
