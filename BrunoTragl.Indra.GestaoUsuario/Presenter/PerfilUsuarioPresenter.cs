using BrunoTragl.Indra.GestaoUsuario.Data.Interfaces;
using BrunoTragl.Indra.GestaoUsuario.DTO;
using BrunoTragl.Indra.GestaoUsuario.IO;
using BrunoTragl.Indra.GestaoUsuario.IO.Interfaces;
using BrunoTragl.Indra.GestaoUsuario.Models;
using BrunoTragl.Indra.GestaoUsuario.Presenter.Interafaces;
using System;
using System.Collections.Generic;

namespace BrunoTragl.Indra.GestaoUsuario.Presenter
{
    public class PerfilUsuarioPresenter : IPerfilUsuarioPresenter
    {
        private readonly IPerfilUsuarioData _perfilUsuarioData;
        private readonly IUsuarioPerfilManipulation _usuarioPerfilManipulation;
        public PerfilUsuarioPresenter(IPerfilUsuarioData perfilUsuarioData, IUsuarioPerfilManipulation usuarioPerfilManipulation)
        {
            _perfilUsuarioData = perfilUsuarioData;
            _usuarioPerfilManipulation = usuarioPerfilManipulation;
        }

        public void ImportarPerfis()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Importando perfis de usuário");
                Console.WriteLine();
                IList<UsuarioPerfilDto> usuarioPerfis = _usuarioPerfilManipulation.GetUsuarioPerfilData();
                IList<PerfilUsuarioModel> perfisUsuarioModel = new List<PerfilUsuarioModel>();
                foreach (var usuarioPerfil in usuarioPerfis)
                {
                    perfisUsuarioModel.Add(new PerfilUsuarioModel
                    {
                        Id = usuarioPerfil.PerfilId,
                        Nome = usuarioPerfil.PerfilNome,
                        Status = new StatusModel
                        {
                            Id = usuarioPerfil.StatusUsuario
                        }
                    });
                    Console.Write(".");
                }
                Console.WriteLine();
                Console.WriteLine();
                _perfilUsuarioData.AddRange(perfisUsuarioModel);
                Console.WriteLine();
                Console.WriteLine("Perfis de usuário importados com sucesso!");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
