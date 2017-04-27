using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFWebClient.UI.Web.Models
{
    public class ViewModelTest
    {
        //public string ClasseString { get; set; }
        //public double ClasseDouble { get; set; }
        //public Classe ClasseExemplo { get; set; }
        //public bool ClasseBoolean { get; set; }
        //public List<int> ListaInteiros { get; set; }
        public List<Classe> ListaClasses { get; set; }
        //public IDictionary<int, Classe> Dicionario { get; set; }
        //public int PropertyInteiro { get; set; }
    }

    public class Classe
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<int> ListaInt { get; set; }
        //public ClasseNeta ClasseNetaExemplo { get; set; }
    }

    public class ClasseNeta
    {
        public string harou { get; set; }
        public string sono { get; set; }
        
    }
}