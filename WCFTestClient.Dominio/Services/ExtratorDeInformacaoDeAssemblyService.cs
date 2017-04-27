using System;
using System.Collections.Generic;
using System.Reflection;
using WCFWebClient.Dominio.Dominio;

namespace WCFWebClient.Dominio.Services
{
    public class ExtratorDeInformacaoDeAssemblyService
    {
        public ExtratorDeInformacaoDeAssemblyService()
        {
        }

        public List<Type> RetornaClassesValidas(string nomeAssembly, Type tipoInterface)
        {
            return new ExtratorDeInformacaoDeAssembly(nomeAssembly).RetornaClassesValidas(tipoInterface);
        }

        public List<Type> RetornaInterfacesValidas(string nomeAssembly)
        {
            return new ExtratorDeInformacaoDeAssembly(nomeAssembly).RetornaInterfacesValidas();
        }

        public List<MethodInfo> RetornaMetodosPorInterface(string nomeAssembly, Type tipoInterface)
        {
            return new ExtratorDeInformacaoDeAssembly(nomeAssembly).RetornaMetodosPorInterface(tipoInterface);
        }

        public List<string> RetornaMetodosString(string nomeAssembly, Type tipoInterface)
        {
            return new ExtratorDeInformacaoDeAssembly(nomeAssembly).RetornaMetodosString(tipoInterface);
        }

        public Type RetornaTipoInterface(string nomeAssembly, string nomeInterface)
        {
            return new ExtratorDeInformacaoDeAssembly(nomeAssembly).RetornaTipoInterface(nomeInterface);
        }

        public ParameterInfo[] RetornaObjetosDoParametroDoMetodo(string nomeAssembly, string nomeInterface, string nomeMetodo)
        {
            return new ExtratorDeInformacaoDeAssembly(nomeAssembly).RetornaObjetosDoParametroDoMetodo(nomeInterface, nomeMetodo);
        }

        public object[] RetornaObjetos(string nomeAssembly, string nomeInterface, string nomeMetodo)
        {
            return new ExtratorDeInformacaoDeAssembly(nomeAssembly).RetornaObjetos(nomeInterface, nomeMetodo);
        }

        public Type[] RetornaTipoDoParametroDoMetodo(string nomeAssembly, Type tipoClasse, string nomeMetodo)
        {
            return new ExtratorDeInformacaoDeAssembly(nomeAssembly).RetornaTipoDoParametroDoMetodo(tipoClasse, nomeMetodo);
        }

        public object CriaObjeto(string nomeAssembly, string nomeTipo)
        {
            return new ExtratorDeInformacaoDeAssembly(nomeAssembly).CriaObjeto(nomeTipo);
        }
    }
}
