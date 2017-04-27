using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCFWebClient.Reflection.Servico.Test
{
    [ServiceContract]
    public interface IContratoWCF
    {
        [OperationContract]
        bool ServicoTest(DadosContratoTest dadosContratoTest);
        bool HarouTest(DadosContratoTest dadosContratoTest);
    }
}
