using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFWebClient.Dominio.Dominio;

namespace WCFWebClient.Dominio.Services
{
    public class CampoObjetoService
    {
        public CampoObjeto CriaCampoObjeto(Type tipo, string nome, string caminhoGenealogicoPai)
        {
            return new CampoObjeto(tipo, nome, caminhoGenealogicoPai);
        }

        public void PreencheListaCampoObjeto(CampoObjeto campoObjeto, List<CampoObjeto> lista)
        {
            campoObjeto.Lista = lista;
        }
    }
}
