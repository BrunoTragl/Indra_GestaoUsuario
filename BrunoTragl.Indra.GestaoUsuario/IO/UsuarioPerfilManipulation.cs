using BrunoTragl.Indra.GestaoUsuario.DTO;
using BrunoTragl.Indra.GestaoUsuario.IO.Base;
using BrunoTragl.Indra.GestaoUsuario.IO.Interfaces;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace BrunoTragl.Indra.GestaoUsuario.IO
{
    public class UsuarioPerfilManipulation : Streamer, IUsuarioPerfilManipulation
    {
        public IList<UsuarioPerfilDto> GetUsuarioPerfilData()
        {
            try
            {
                return (IList<UsuarioPerfilDto>) GetFile("UsuarioPerfilFileName");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override object ResolveParser(TextFieldParser csvParser)
        {
            try
            {
                IList<UsuarioPerfilDto> list = new List<UsuarioPerfilDto>();
                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    UsuarioPerfilDto usuario = new UsuarioPerfilDto();
                    usuario.NomeUsuario = fields[0];
                    DateTime dataNascimento;
                    if (DateTime.TryParse(fields[1], out dataNascimento))
                        usuario.DataNascimento = dataNascimento;
                    usuario.PerfilId = int.Parse(fields[2]);
                    usuario.PerfilNome = fields[3];
                    usuario.PerfilStatus = int.Parse(fields[4]);
                    usuario.StatusUsuario = int.Parse(fields[5]);
                    list.Add(usuario);
                }
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
