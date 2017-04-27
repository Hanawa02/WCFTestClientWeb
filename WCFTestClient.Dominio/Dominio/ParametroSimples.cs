using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFWebClient.Dominio.Dominio
{
    public class ParametroSimples
    {
        public string Campo { get; set; }

        public string Valor { get; set; }

        public ParametroSimples(string campo, string valor)
        {
            this.Campo = campo;

            this.Valor = valor;
        }
    }
}
