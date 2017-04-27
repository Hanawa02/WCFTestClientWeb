using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFWebClient.Reflection.Servico.Test
{
    public class ServicoWCF : IContratoWCF
    {
        public bool ServicoTest(DadosContratoTest dadosContratoTest)
        {
            return true;
        }

        public bool HarouTest(DadosContratoTest dadosContratoTest)
        {
            return true;
        }
    }
}
