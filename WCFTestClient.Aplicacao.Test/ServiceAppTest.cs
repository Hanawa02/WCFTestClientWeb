using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace WCFWebClient.Aplicacao.Test
{
    [TestClass]
    public class ServiceAppTest
    {
        [TestMethod]
        public void App_TestConstructor()
        {
            // Dominio
            /*var contract = new ReflectionUtilService();
            // Aplicacao
            var instance = new ServiceApp(contract);

            var list = instance.GetAll("qualquer Coisa");

            var resultado = list.Where(x => x.Tipo == (typeof(int))).FirstOrDefault();

            Assert.AreEqual(typeof(int), resultado.Tipo, "não carregou o tipo correto");
            Assert.AreEqual("Nome", resultado.Nome);

            var resultado2 = list.Where(x => x.Tipo == (typeof(CampoObjeto))).FirstOrDefault();

            Assert.AreEqual(typeof(CampoObjeto), resultado2.Tipo, "não carregou o tipo correto");
            Assert.AreEqual("Nome3", resultado2.Nome);

            var resultado3 = list.Where(x => x.Tipo == (typeof(string))).FirstOrDefault();

            Assert.AreEqual(typeof(string), resultado3.Tipo, "não carregou o tipo correto");
            Assert.AreEqual("Nome2", resultado3.Nome);

            var resultado4 = list.Where(x => x.Tipo == (typeof(bool))).FirstOrDefault();

            Assert.AreEqual(typeof(bool), resultado4.Tipo, "não carregou o tipo correto");
            Assert.AreEqual("Nome4", resultado4.Nome);

            var resultado5 = list.Where(x => x.Tipo == (typeof(KeyValuePair<string, int>))).FirstOrDefault();

            Assert.AreEqual(typeof(KeyValuePair<string, int>), resultado5.Tipo, "não carregou o tipo correto");
            Assert.AreEqual("ChaveValor", resultado5.Nome);

            var resultado6 = list.Where(x => x.Tipo == (typeof(IList<MultiValor>))).FirstOrDefault();

            Assert.AreEqual(typeof(IList<MultiValor>), resultado6.Tipo, "não carregou o tipo correto");
            Assert.AreEqual("Lista", resultado6.Nome); */
        }

        [TestMethod]
        public void Teste_ObjetoComplexo()
        {
            var type = typeof(Classe);
        }
    }

    public class Classe
    {
        public int Inteiro { get; set; }
        public ClasseFilha Filha { get; set; }

    }
    public class ClasseFilha
    {
        public string Literal { get; set; }
    }
}
