using System.Collections.Generic;
using WCFWebClient.Dominio.Data;

namespace WCFWebClient.Aplicacao.Contrato
{
    public interface IServiceData
    {
        Perfil RetornaPerfilPorId(int idPerfil);

        List<Perfil> RetornaListaDePerfis(string nomeMetodo, string nomeInterface);

        bool SalvaPerfil(string descricao, string nomeMetodo, string nomeInterface, Dictionary<string, string> parametros);

        List<Parametro> RetornaParametrosDoPerfil(int perfilId);
    }
}
