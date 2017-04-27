using System;
using System.Collections.Generic;
using System.Reflection;
using WCFWebClient.Dominio.Dominio;

namespace WCFWebClient.Aplicacao.Contrato
{
    public interface IServiceApp
    {
        IList<CampoObjeto> GetAll(string url);

        CampoObjeto CriaCampoObjeto(ParameterInfo[] objetos, string nomeMetodo);

        List<CampoObjeto> GetAllByObject(Type tipoObjeto);

        void DefinePropriedade(ref object instancia, string nomePropriedade, object valor);
    }
}
