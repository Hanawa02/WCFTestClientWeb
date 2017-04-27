using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFWebClient.Dominio.Data;

namespace WCFWebClient.Infra.Data.Repository.Contratos
{
    public interface IPerfilRepository
    {
        Perfil SalvaPerfil(Perfil perfil);

        Perfil RetornaPerfilPorId(int perfilId);

        List<Perfil> RetornaListaDePerfis(string nomeMetodo, string nomeInterface);
    }
}
