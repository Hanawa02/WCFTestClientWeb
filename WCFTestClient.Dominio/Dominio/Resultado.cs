using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFWebClient.Dominio.Dominio
{
    public class Resultado
    {
        public string TipoDoReultado { get; set; }

        public List<string> Mensagens { get; private set; }
        

        public Resultado(string tipoDoReultado)
        {
            this.TipoDoReultado = tipoDoReultado;

            this.Mensagens = new List<string>();
        }

        public void AdicionaMensagem(string mensagem)
        {
            this.Mensagens.Add(mensagem);
        }
    }
}
