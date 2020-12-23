using System;

namespace BrunoTragl.Indra.GestaoUsuario.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public PerfilUsuarioModel PerfilUsuario { get; set; }
    }
}
