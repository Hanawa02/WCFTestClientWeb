using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFWebClient.Dominio.Dominio;

namespace WCFWebClient.Dominio.Services
{
    public class InvocadorDeServicoService
    {
        public Resultado InvoqueMetodo(string nomeMetodo, object[] parametro, string nomeInterface, string endpointServico)
        {
            return new InvocadorDeServico().InvoqueMetodo(nomeMetodo, parametro, nomeInterface, endpointServico);
        }

        public void AdicionaPropriedadeEValorDoObjetoComoMensagem(object item)
        {
            new InvocadorDeServico().AdicionaPropriedadeEValorDoObjetoComoMensagem(item);
        }
    }
}
