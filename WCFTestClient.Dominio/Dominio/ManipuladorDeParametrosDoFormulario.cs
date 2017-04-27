using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WCFWebClient.Dominio.Data;
using WCFWebClient.Dominio.Services;

namespace WCFWebClient.Dominio.Dominio
{
    public class ManipuladorDeParametrosDoFormulario
    {
        public FileStream SerializaCampoObjeto(CampoObjeto campoObjeto, string nomeArquivo)
        {
            try
            {
                FileStream fileStream = new FileStream(@"C:\Temp\" + nomeArquivo + ".bin", FileMode.Create);

                BinaryFormatter binaryFormatter = new BinaryFormatter();

                binaryFormatter.Serialize(fileStream, campoObjeto);

                fileStream.Close();

                return fileStream;
            }
            catch (IOException ioExeption)
            {
                throw ioExeption;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public CampoObjeto DeserializaCampoObjeto(FileStream arquivo)
        {
            FileInfo fileInfo = new FileInfo(@arquivo.Name);

            if (fileInfo.Exists)
            {
                FileStream fileStream = new FileStream(@arquivo.Name, FileMode.Open);

                BinaryFormatter binaryFormatter = new BinaryFormatter();

                CampoObjeto campoObjeto = (CampoObjeto)binaryFormatter.Deserialize(fileStream);

                fileStream.Close();

                return campoObjeto;
            }
            else
            {
                return null;
            }
        }
    }
}