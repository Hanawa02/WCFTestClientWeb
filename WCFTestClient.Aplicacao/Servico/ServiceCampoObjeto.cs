using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFWebClient.Dominio.Dominio;
using WCFWebClient.Dominio.Contratos.Service;
using WCFWebClient.Aplicacao.Contrato;
using WCFWebClient.Dominio.Services;

namespace WCFWebClient.Aplicacao.Servico
{
    public class ServiceCampoObjeto : IServiceCampoObjeto
    {
        CampoObjetoService Service;

        public ServiceCampoObjeto()
        {
            Service = new CampoObjetoService();
        }
        
        public CampoObjeto CriaCampoObjeto(Type tipo, string nome, string caminhoGenealogicoPai)
        {
            return Service.CriaCampoObjeto(tipo, nome, caminhoGenealogicoPai);
        }

        public void PreencheListaCampoObjeto(CampoObjeto campoObjeto, List<CampoObjeto> lista)
        {
            Service.PreencheListaCampoObjeto(campoObjeto, lista);
        }
    }
}
