using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFWebClient.Dominio.Data
{
    public class Perfil
    {
        public int PerfilId {get;set;}
        public string Descricao { get; set; }
        public string Servico { get; set; }
        public string Capacidade { get; set; }
        public IList<Parametro> Parametros { get; set; }
    }
}
