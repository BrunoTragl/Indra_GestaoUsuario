using BrunoTragl.Indra.GestaoUsuario.Data.Interfaces;
using BrunoTragl.Indra.GestaoUsuario.DTO;
using BrunoTragl.Indra.GestaoUsuario.IO;
using BrunoTragl.Indra.GestaoUsuario.IO.Interfaces;
using BrunoTragl.Indra.GestaoUsuario.Models;
using BrunoTragl.Indra.GestaoUsuario.Presenter.Interafaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BrunoTragl.Indra.GestaoUsuario.Presenter
{
    public class UsuarioPresenter : IUsuarioPresenter
    {
        private readonly IUsuarioData _usuarioData;
        private readonly IStatusData _statusData;
        private readonly IPerfilUsuarioData _perfilUsuarioData;
        private readonly IUsuarioPerfilManipulation _usuarioPerfilManipulation;
        public UsuarioPresenter(IUsuarioData usuarioData, 
                                IUsuarioPerfilManipulation usuarioPerfilManipulation, 
                                IPerfilUsuarioData perfilUsuarioData,
                                IStatusData statusData)
        {
            _usuarioData = usuarioData;
            _usuarioPerfilManipulation = usuarioPerfilManipulation;
            _perfilUsuarioData = perfilUsuarioData;
            _statusData = statusData;
        }

        public void ImportarUsuarios()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Importando usuários");
                Console.WriteLine();
                IList<UsuarioPerfilDto> usuarios = _usuarioPerfilManipulation.GetUsuarioPerfilData();
                IList<UsuarioModel> usuariosModel = new List<UsuarioModel>();
                foreach (var usuario in usuarios)
                {
                    usuariosModel.Add(new UsuarioModel
                    {
                        Nome = usuario.NomeUsuario,
                        DataNascimento = usuario.DataNascimento,
                        PerfilUsuario = new PerfilUsuarioModel
                        {
                            Id = usuario.PerfilId,
                            Nome = usuario.PerfilNome,
                            Status = new StatusModel
                            {
                                Id = usuario.StatusUsuario
                            }
                        }
                    });
                    Console.Write(".");
                }
                Console.WriteLine();
                Console.WriteLine();
                _usuarioData.AddRange(usuariosModel);
                Console.WriteLine();
                Console.WriteLine("Usuários importados com sucesso!");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListarUsuariosComIdImpar()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine($"Usuários com id ímpar:");
                IList<UsuarioModel> usuariosModel = _usuarioData.List();
                foreach (var usuario in usuariosModel)
                    if (usuario.Id % 2 != 0)
                        Console.WriteLine($"{usuario.Id} {usuario.Nome}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AlterarNomeDeUsuariosComIniciaisSr()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine($"Usuários com iniciais SR no nome:");
                IList<UsuarioModel> usuariosModel = _usuarioData.List();
                foreach (var usuario in usuariosModel)
                    if (usuario.Nome.StartsWith("SR"))
                    {
                        Console.WriteLine($"{usuario.Id} {usuario.Nome}");
                        usuario.Nome = usuario.Nome.Substring(3, usuario.Nome.Length - 3);
                        _usuarioData.Edit(usuario);
                        Console.WriteLine($"Alterado para {usuario.Nome}");
                    }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListarUsuariosComPerfilAdministrador()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine($"Usuários com perfil de 'ADM' (Administrador):");

                IList<PerfilUsuarioModel> perfilUsuariosModel = _perfilUsuarioData.List();
                PerfilUsuarioModel PerfilAdminitrador = perfilUsuariosModel.Where(p => p.Nome == "ADM").FirstOrDefault();

                if (PerfilAdminitrador == null)
                    throw new Exception("Não foi localizado nenhum perfil de usuário com nome de 'ADM' (administrador).");

                IList<UsuarioModel> usuariosModel = _usuarioData.List();
                foreach (var usuario in usuariosModel)
                    if (usuario.PerfilUsuario.Id == PerfilAdminitrador.Id)
                        Console.WriteLine($"{usuario.Id} {usuario.Nome}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void QuantidadeUsuariosInativos()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine($"Quantidade de usuários inativos:");
                IList<StatusModel> listStatus = _statusData.List();
                IList<PerfilUsuarioModel> listPerfilUsuario = _perfilUsuarioData.List();
                IList<UsuarioModel> usuariosModel = _usuarioData.List();
                int quantidadeUsuariosInativos = 0;
                foreach (var usuario in usuariosModel)
                {
                    PerfilUsuarioModel perfilUsuario = listPerfilUsuario.Where(pu => pu.Id == usuario.PerfilUsuario.Id).FirstOrDefault();
                    if (perfilUsuario == null)
                    {
                        Console.WriteLine($"Não foi possível localizar o perfil do usuario {usuario.Id} {usuario.Nome}");
                        continue;
                    }

                    StatusModel status = listStatus.Where(s => s.Id == perfilUsuario.Status.Id).FirstOrDefault();
                    if (status == null)
                    {
                        Console.WriteLine($"Não foi possível localizar o status do usuario {usuario.Id} {usuario.Nome}");
                        continue;
                    }

                    if (status.Nome == "INATIVO")
                        quantidadeUsuariosInativos++;
                }
                Console.WriteLine(quantidadeUsuariosInativos);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
