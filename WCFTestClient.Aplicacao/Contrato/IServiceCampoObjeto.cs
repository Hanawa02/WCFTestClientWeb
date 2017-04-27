using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFWebClient.Dominio.Dominio;

namespace WCFWebClient.Aplicacao.Contrato
{
    public interface IServiceCampoObjeto
    {
        CampoObjeto CriaCampoObjeto(Type tipo, string nome, string caminhoGenealogicoPai);

        void PreencheListaCampoObjeto(CampoObjeto campoObjeto, List<CampoObjeto> lista);
        
    }
}
