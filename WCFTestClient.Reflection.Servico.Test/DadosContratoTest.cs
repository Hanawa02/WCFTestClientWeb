using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCFWebClient.Reflection.Servico.Test
{
    [DataContract]
    public class DadosContratoTest
    {
        [DataMember]
        public int Teste { get; set; }
    }
}
