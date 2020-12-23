using System;

namespace BrunoTragl.Indra.GestaoUsuario.DTO
{
    public class UsuarioPerfilDto
    {
        public string NomeUsuario { get; set; }
        public DateTime DataNascimento { get; set; }
        public int PerfilId { get; set; }
        public string PerfilNome { get; set; }
        public int PerfilStatus { get; set; }
        public int StatusUsuario { get; set; }
    }
}
