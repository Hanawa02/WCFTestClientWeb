using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCFWebClient.Dominio.Dominio;

namespace WCFWebClient.Domain.Test
{
    [TestClass]
    public class ReflectionTest
    {
        [TestMethod]
        public void  Tipo_ObterTiposObjetos()
        {
            //var instancia = new ClasseTeste();
            //AAA
            var gerenciadorReflection = new ReflectionUtil(typeof(ClasseTeste));
            //Tipo, Nome
            var propriedades = gerenciadorReflection.RetornaPropriedades();
            //var resultado = propriedades.Where(x => x.Tipo == (typeof(int))).FirstOrDefault();

            // Assert.AreEqual(typeof(int), resultado.Tipo, "não carregou o tipo correto");
            //Assert.AreEqual("Nome", resultado.Nome);

           


        }        
    }

    public class ClasseTeste
    {
        public CampoObjeto Nome3 { get; set; }
        public string Nome2 { get; set; }
        public int Nome { get; set; }
        public bool Nome4 { get; set; }
        public KeyValuePair<string, int> ChaveValor { get; set; }
        public IList<MultiValor> Lista { get; set; }
    }

    public class MultiValor
    {
        public int Chave { get; set; }
        public string Descricao { get; set; }
    }

}
