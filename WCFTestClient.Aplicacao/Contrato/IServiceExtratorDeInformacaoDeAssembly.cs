using System;
using System.Collections.Generic;
using System.Reflection;

namespace WCFWebClient.Aplicacao.Contrato
{
    public interface IServiceExtratorDeInformacaoDeAssembly
    {
        List<Type> RetornaClassesValidas(string nomeAssembly, Type tipoInterface);

        List<Type> RetornaInterfacesValidas(string nomeAssembly);

        List<MethodInfo> RetornaMetodosPorInterface(string nomeAssembly, Type tipoInterface);

        List<string> RetornaMetodosString(string nomeAssembly, Type tipoInterface);

        Type RetornaTipoInterface(string nomeAssembly, string nomeInterface);

        ParameterInfo[] RetornaObjetosDoParametroDoMetodo(string nomeAssembly, string nomeInterface, string nomeMetodo);

        Type[] RetornaTipoDoParametroDoMetodo(string nomeAssembly, Type tipoClasse, string nomeMetodo);

        object CriaObjeto(string nomeAssembly, string nomeTipo);
    }
}
