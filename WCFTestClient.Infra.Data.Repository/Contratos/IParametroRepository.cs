using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFWebClient.Dominio.Data;

namespace WCFWebClient.Infra.Data.Repository.Contratos
{
    public interface IParametroRepository
    {
        void SalvaParametro(Parametro parametro);

        List<Parametro> RetornaListaDeParametros(int perfilId);
    }
}
