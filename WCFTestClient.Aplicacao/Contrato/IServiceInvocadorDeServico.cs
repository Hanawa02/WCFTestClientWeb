using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFWebClient.Dominio.Dominio;

namespace WCFWebClient.Aplicacao.Contrato
{
    public interface IServiceInvocadorDeServico
    {
        Resultado InvoqueMetodo(string nomeMetodo, object[] parametro, string nomeInterface, string endpointServico);

        void AdicionaPropriedadeEValorDoObjetoComoMensagem(object item);
    }
}
