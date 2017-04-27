using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFWebClient.Dominio.Services
{
    public class DTOServico
    {
        public int Inteiro { get; set; }
        public string String { get; set; }
        public bool Bool { get; set; }
        public KeyValuePair<string, int> ChaveValor { get; set; }
        public IList<MultiValor> Lista { get; set; }
        public DTOServico()
        {
        }
    }

    public class MultiValor
    {
        public int Chave { get; set; }
        public string Descricao { get; set; }
    }
}
