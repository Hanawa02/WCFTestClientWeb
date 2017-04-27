using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.ServiceModel;

namespace WCFWebClient.Reflection.Servico.Test
{
    /// <summary>
    /// Summary description for ServicosTest
    /// </summary>
    [TestClass]
    public class ServicosTest
    {       
        [TestMethod]
        public void AtributoQualificadoTest()
        {
            //Todas as interfaces que tenha o atributo customizado ServiceContract
            
            var assembly = Assembly.Load("WCFWebClient.Reflection.Servico.Test");

            var listaDeInterfacesValidas = RetornaInterfacesValidas(assembly);

            Assert.IsTrue(listaDeInterfacesValidas.Any());
        }
            
        [TestMethod]
        public void ClasseQueImplementaAhInterface()
        {
            //Todas as classes que implenta a interface que tenha o atributo customizado ServiceContract

            var assembly = Assembly.Load("WCFWebClient.Reflection.Servico.Test");

            var classes = RetornaClassesValidas(assembly);

            Assert.IsTrue(classes.Any());
        }

        public Dictionary<Type, Type> RetornaClassesValidas(Assembly assembly)
        {
            var interfaces = RetornaInterfacesValidas(assembly);

            var classes = new Dictionary<Type, Type>();

            foreach (var item in interfaces)
            {
                List<Type> listaDeClasses = assembly.GetTypes().Where(x => item.IsAssignableFrom(x) && x.IsClass).ToList();

                foreach (var classe in listaDeClasses)
                {
                    classes.Add(item, classe);
                }
            }

            return classes;
        }


        public List<Type> RetornaInterfacesValidas(Assembly assembly)
        {
            var listaDeInterfaces = new List<Type>();

            foreach (Type tipo in assembly.GetTypes().Where(x => x.IsInterface))
            {
                var atributo = tipo.GetCustomAttributes(typeof(ServiceContractAttribute), false);

                if (atributo != null && atributo.Length > 0)
                {
                    listaDeInterfaces.Add(tipo);
                }
            }

            return listaDeInterfaces;
        }
    }
}
