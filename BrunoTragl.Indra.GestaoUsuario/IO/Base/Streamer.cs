using Microsoft.VisualBasic.FileIO;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace BrunoTragl.Indra.GestaoUsuario.IO.Base
{
    public abstract class Streamer
    {
        protected object GetFile(string settingName)
        {
            try
            {
                using (TextFieldParser csvParser = new TextFieldParser(GetFileName(settingName)))
                {
                    csvParser.SetDelimiters(new string[] { ";" });
                    csvParser.HasFieldsEnclosedInQuotes = true;
                    csvParser.ReadLine();
                    return ResolveParser(csvParser);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetFileName(string settingName)
        {
            string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileName = ConfigurationManager.AppSettings[settingName];
            return Path.Combine(appDirectory, fileName);
        }

        protected abstract object ResolveParser(TextFieldParser csvParser);
    }
}
