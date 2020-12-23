using BrunoTragl.Indra.GestaoUsuario.DTO;
using BrunoTragl.Indra.GestaoUsuario.IO.Base;
using BrunoTragl.Indra.GestaoUsuario.IO.Interfaces;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace BrunoTragl.Indra.GestaoUsuario.IO
{
    public class StatusManipulation : Streamer, IStatusManipulation
    {
        public IList<StatusDto> GetStatusData()
        {
            try
            {
                return (IList<StatusDto>) GetFile("StatusFileName");
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
                IList<StatusDto> list = new List<StatusDto>();
                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    StatusDto status = new StatusDto();
                    status.Id = int.Parse(fields[0]);
                    status.Nome = fields[1];
                    DateTime dataCriacao;
                    if (DateTime.TryParse(fields[2], out dataCriacao))
                        status.DataCriacao = dataCriacao;
                    list.Add(status);
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
