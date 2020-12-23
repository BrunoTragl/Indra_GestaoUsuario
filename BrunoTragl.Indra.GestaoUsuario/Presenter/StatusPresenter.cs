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
    public class StatusPresenter : IStatusPresenter
    {
        private readonly IStatusData _statusData;
        private readonly IStatusManipulation _statusManipulation;
        public StatusPresenter(IStatusData usuarioData, IStatusManipulation statusManipulation)
        {
            _statusData = usuarioData;
            _statusManipulation = statusManipulation;
        }

        public void ImportarStatus()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Importando status");
                Console.WriteLine();
                IList<StatusDto> manyStatus = _statusManipulation.GetStatusData();
                IList<StatusModel> manyStatusModel = new List<StatusModel>();
                foreach (var status in manyStatus)
                {
                    manyStatusModel.Add(new StatusModel
                    {
                        Id = status.Id,
                        Nome = status.Nome,
                        Criacao = status.DataCriacao
                    });
                    Console.Write(".");
                }
                Console.WriteLine();
                Console.WriteLine();
                _statusData.AddRange(manyStatusModel);
                Console.WriteLine();
                Console.WriteLine("Status importados com sucesso!");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
