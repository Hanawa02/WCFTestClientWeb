using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;

namespace WCFWebClient.Dominio.Dominio
{
    public class ExtratorDeInformacaoDeAssembly
    {
        public Assembly _Assembly { get; private set; }

        public ExtratorDeInformacaoDeAssembly(String NomeAssembly)
        {
            this._Assembly = Assembly.Load(NomeAssembly);
        }

        public List<Type> RetornaClassesValidas(Type tipoInterface)
        {
            List<Type> listaDeClasses = this._Assembly.GetTypes().Where(x => tipoInterface.IsAssignableFrom(x) && x.IsClass).ToList();

            return listaDeClasses;
        }

        public List<Type> RetornaInterfacesValidas()
        {
            var listaDeInterfaces = new List<Type>();

            foreach (Type tipo in this._Assembly.GetTypes().Where(x => x.IsInterface))
            {
                var atributo = tipo.GetCustomAttributes(typeof(ServiceContractAttribute), false);

                if (atributo != null && atributo.Length > 0)
                {
                    listaDeInterfaces.Add(tipo);
                }
            }
            List<Type> listaDeInterfacesOrdenada = listaDeInterfaces.OrderBy(x => x.Name).ToList();

            return listaDeInterfacesOrdenada;
        }

        public List<MethodInfo> RetornaMetodosPorInterface(Type tipoInterface)
        {
            if (tipoInterface != null)
            {
                return tipoInterface.GetMethods().ToList();
            }
            else
            {
                return null;
            }
        }

        public List<string> RetornaMetodosString(Type tipoInterface)
        {
            var listaString = new List<string>();

            if (tipoInterface != null)
            {
                var listaMethodInfo = RetornaMetodosPorInterface(tipoInterface);

                listaMethodInfo.ForEach(x => listaString.Add(x.Name));
            }

            listaString.Sort();

            return listaString;
        }

        public Type RetornaTipoInterface(string nomeInterface)
        {
            var nameSpaceInterface = this._Assembly.GetName().ToString().Split(',')[0] + "." + nomeInterface;
            if (!string.IsNullOrWhiteSpace(nomeInterface))
            {

                var interfaceSelecionada = this._Assembly.GetTypes().Where(i => i.IsInterface && i.Namespace == nameSpaceInterface).First();

                return interfaceSelecionada;
            }
            else
            {
                return null;
            }
        }

        public ParameterInfo[] RetornaObjetosDoParametroDoMetodo(string nomeInterface, string nomeMetodo)
        {
            Type tipoInterface = this.RetornaTipoInterface(nomeInterface);

            List<Type> classeImplementadora = RetornaClassesValidas(tipoInterface);

            Type tipoClasse = classeImplementadora.FirstOrDefault();

            var metodos = RetornaMetodosPorInterface(tipoClasse);

            MethodInfo metodoSelecionado = null;

            metodos.ForEach(metodo =>
            {
                if (metodo.Name == nomeMetodo)
                {
                    metodoSelecionado = metodo;
                    return;
                }
            });

            ParameterInfo[] parametros = metodoSelecionado.GetParameters();

            int quantidadeParametros = parametros.Length - 2;

            ParameterInfo[] objetos = new ParameterInfo[quantidadeParametros];

            for (int i = 2; i < parametros.Length; i++)
            {
                objetos[i - 2] = parametros[i];
            }

            return objetos;
        }

        public object[] RetornaObjetos(string nomeInterface, string nomeMetodo)
        {
            var parametros = RetornaObjetosDoParametroDoMetodo(nomeInterface, nomeMetodo);

            var objetos = new object[parametros.Length];

            for (int i = 0; i < parametros.Length; i++)
            {                
                if (parametros[i].ParameterType == typeof(string))
                {
                    objetos[i] = string.Empty;
                }
                else
                {
                    objetos[i] = Activator.CreateInstance(parametros[i].ParameterType);
                }
            }

            return objetos;
        }

        public object RetornaObjetoAutenticador(string nomeInterface, string nomeMetodo)
        {
            Type tipoInterface = this.RetornaTipoInterface(nomeInterface);

            List<Type> classeImplementadora = RetornaClassesValidas(tipoInterface);

            Type tipoClasse = classeImplementadora.FirstOrDefault();

            var metodos = RetornaMetodosPorInterface(tipoClasse);

            MethodInfo metodoSelecionado = null;

            metodos.ForEach(metodo =>
            {
                if (metodo.Name == nomeMetodo)
                {
                    metodoSelecionado = metodo;
                    return;
                }
            });

            ParameterInfo[] parametros = metodoSelecionado.GetParameters();

            Type tipoParametro = parametros[1].ParameterType;

            var Objeto = Activator.CreateInstance(tipoParametro);

            return Objeto;
        }


        public Type RetornaTipoAutenticador(string nomeInterface, string nomeMetodo)
        {
            Type tipoInterface = this.RetornaTipoInterface(nomeInterface);

            List<Type> classeImplementadora = RetornaClassesValidas(tipoInterface);

            Type tipoClasse = classeImplementadora.FirstOrDefault();

            var metodos = RetornaMetodosPorInterface(tipoClasse);

            MethodInfo metodoSelecionado = null;

            metodos.ForEach(metodo =>
            {
                if (metodo.Name == nomeMetodo)
                {
                    metodoSelecionado = metodo;
                    return;
                }
            });

            ParameterInfo[] parametros = metodoSelecionado.GetParameters();

            Type tipoParametro = parametros[1].ParameterType;

            return tipoParametro;
        }

        public Type[] RetornaTipoDoParametroDoMetodo(Type tipoClasse, string nomeMetodo)
        {
            var metodos = RetornaMetodosPorInterface(tipoClasse);

            MethodInfo metodoSelecionado = null;

            metodos.ForEach(metodo =>
            {
                if (metodo.Name == nomeMetodo)
                {
                    metodoSelecionado = metodo;
                    return;
                }
            });

            ParameterInfo[] parametros = metodoSelecionado.GetParameters();

            Type[] tipoParametro = (Type[])Array.CreateInstance(typeof(Type), (parametros.Length - 2));

            for (int i = 2; i < parametros.Length; i++)
            {
                tipoParametro[i - 2] = parametros[i].ParameterType;
            }

            return tipoParametro;
        }

        public object CriaObjeto(string fullNameType)
        {
            var tipo = this._Assembly.GetTypes().Where(i => i.FullName == fullNameType).First();

            var objeto = Activator.CreateInstance(tipo);

            return objeto;
        }
    }
}