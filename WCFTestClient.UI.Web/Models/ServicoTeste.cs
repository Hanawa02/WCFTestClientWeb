using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFWebClient.UI.Web.Models
{
    public class ServicoTeste : IContratoTeste
    {
        public bool Servico(ViewModelTest modelTeste)
        {
            return true;
        }
    }
}