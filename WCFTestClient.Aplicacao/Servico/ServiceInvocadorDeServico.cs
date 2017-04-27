using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFWebClient.Aplicacao.Contrato;
using WCFWebClient.Dominio.Dominio;
using WCFWebClient.Dominio.Services;

namespace WCFWebClient.Aplicacao.Servico
{
    public class ServiceInvocadorDeServico : IServiceInvocadorDeServico
    {
        InvocadorDeServicoService Service;

        public ServiceInvocadorDeServico()
        {
            Service = new InvocadorDeServicoService();
        }

        public Resultado InvoqueMetodo(string nomeMetodo, object[] parametro, string nomeInterface, string endpointServico)
        {
            return Service.InvoqueMetodo(nomeMetodo, parametro, nomeInterface, endpointServico);
        }

        public void AdicionaPropriedadeEValorDoObjetoComoMensagem(object item)
        {
            Service.AdicionaPropriedadeEValorDoObjetoComoMensagem(item);
        }
    }
}
