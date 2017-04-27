using System;
using System.Collections.Generic;
using System.Reflection;
using WCFWebClient.Dominio.Dominio;

namespace WCFWebClient.Dominio.Contratos.Service
{
    public class ReflectionUtilService
    {
        public ReflectionUtilService()
        {
        }

        public List<CampoObjeto> RetornaPropriedades(Type tipoObjeto)
        {
            return new ReflectionUtil(tipoObjeto).RetornaPropriedades();
        }

        public CampoObjeto CriaCampoObjeto(ParameterInfo[] objetos, string nomeMetodo)
        {
            return new ReflectionUtil(typeof(object)).CriaCampoObjeto(objetos, nomeMetodo);
        }

        public void DefinePropriedade(ref object instancia, string nomePropriedade, object valor)
        {
            new ReflectionUtil(instancia.GetType()).DefinePropriedade(ref instancia, nomePropriedade, valor);
        }
    }
}
