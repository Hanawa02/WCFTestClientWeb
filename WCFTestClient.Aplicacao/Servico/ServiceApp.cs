using System;
using System.Collections.Generic;
using System.Reflection;
using WCFWebClient.Aplicacao.Contrato;
using WCFWebClient.Dominio.Contratos.Service;
using WCFWebClient.Dominio.Dominio;

namespace WCFWebClient.Aplicacao.Servico
{
    public class ServiceApp : IServiceApp
    {
        ReflectionUtilService Service;

        public ServiceApp()
        {
            Service = new ReflectionUtilService();
        }

        public IList<CampoObjeto> GetAll(string url)
        {
            return new List<CampoObjeto>();
        }

        public CampoObjeto CriaCampoObjeto(ParameterInfo[] objetos, string nomeMetodo)
        {
            return Service.CriaCampoObjeto(objetos, nomeMetodo);
        }

        public List<CampoObjeto> GetAllByObject(Type tipoObjeto)
        {
            return Service.RetornaPropriedades(tipoObjeto);
        }

        public void DefinePropriedade(ref object instancia, string nomePropriedade, object valor)
        {
            Service.DefinePropriedade(ref instancia, nomePropriedade, valor);
        }
    }
}
