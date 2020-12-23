using System;

namespace BrunoTragl.Indra.GestaoUsuario.Models
{
    public class PerfilUsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public StatusModel Status { get; set; }
    }
}
