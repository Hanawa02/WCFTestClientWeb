using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFWebClient.Dominio.Data
{
    public class Parametro
    {
        public int ParametroId { get; set; }
        public string Campo { get; set; }
        public string Valor { get; set; }
        public int PerfilId { get; set; }
        public virtual Perfil Perfil { get; set; }
    }
}
