using System;
using System.Collections.Generic;
using System.Reflection;
using WCFWebClient.Aplicacao.Contrato;
using WCFWebClient.Dominio.Services;

namespace WCFWebClient.Aplicacao.Servico
{
    public class ServiceExtratorDeInformacaoDeAssembly : IServiceExtratorDeInformacaoDeAssembly
    {
        ExtratorDeInformacaoDeAssemblyService Service;

        public ServiceExtratorDeInformacaoDeAssembly()
        {
            Service = new ExtratorDeInformacaoDeAssemblyService();
        }

        public List<Type> RetornaClassesValidas(string nomeAssembly, Type tipoInterface)
        {
            return Service.RetornaClassesValidas(nomeAssembly, tipoInterface);
        }

        public List<Type> RetornaInterfacesValidas(string nomeAssembly)
        {
            return Service.RetornaInterfacesValidas(nomeAssembly);
        }

        public List<MethodInfo> RetornaMetodosPorInterface(string nomeAssembly, Type tipoInterface)
        {
            return Service.RetornaMetodosPorInterface(nomeAssembly, tipoInterface);
        }

        public List<string> RetornaMetodosString(string nomeAssembly, Type tipoInterface)
        {
            return Service.RetornaMetodosString(nomeAssembly, tipoInterface);
        }

        public Type RetornaTipoInterface(string nomeAssembly, string nomeInterface)
        {
            return Service.RetornaTipoInterface(nomeAssembly, nomeInterface);
        }

        public ParameterInfo[] RetornaObjetosDoParametroDoMetodo(string nomeAssembly, string nomeInterface, string nomeMetodo)
        {
            return Service.RetornaObjetosDoParametroDoMetodo(nomeAssembly, nomeInterface, nomeMetodo);
        }

        public object[] RetornaObjetos(string nomeAssembly, string nomeInterface, string nomeMetodo)
        {
            return Service.RetornaObjetos(nomeAssembly, nomeInterface, nomeMetodo);
        }

        public Type[] RetornaTipoDoParametroDoMetodo(string nomeAssembly, Type tipoClasse, string nomeMetodo)
        {
            return Service.RetornaTipoDoParametroDoMetodo(nomeAssembly, tipoClasse, nomeMetodo);
        }

        public object CriaObjeto(string nomeAssembly, string nomeTipo)
        {
            return Service.CriaObjeto(nomeAssembly, nomeTipo);
        }
    }
}
