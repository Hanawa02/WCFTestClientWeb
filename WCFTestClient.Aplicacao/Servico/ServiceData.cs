using System.Collections.Generic;
using WCFWebClient.Aplicacao.Contrato;
using WCFWebClient.Dominio.Data;
using WCFWebClient.Dominio.Dominio;
using WCFWebClient.Infra.Data.Repository.Services;

namespace WCFWebClient.Aplicacao.Servico
{
    public class ServiceData : IServiceData
    {
        PerfilRepository Service;

        public ServiceData()
        {
            Service = new PerfilRepository();
        }

        public List<Perfil> RetornaListaDePerfis(string nomeMetodo, string nomeInterface)
        {
            return Service.RetornaListaDePerfis(nomeMetodo, nomeInterface);
        }

        public Perfil RetornaPerfilPorId(int idPerfil)
        {
            return Service.RetornaPerfilPorId(idPerfil);
        }

        public bool SalvaPerfil(string descricao, string nomeMetodo, string nomeInterface, Dictionary<string, string> parametros)
        {
            var perfilRetorno = Service.SalvaPerfil(new Perfil() { Descricao = descricao, Capacidade = nomeMetodo, Servico = nomeInterface });

            var parametroRepository = new ParametroRepository();

            parametroRepository.RemoveParametros(perfilRetorno.PerfilId);

            foreach (var item in parametros)
            {
                var parametro = new Parametro() { PerfilId = perfilRetorno.PerfilId, Campo = item.Key, Valor = item.Value };

                parametroRepository.SalvaParametro(parametro);
            }

            return perfilRetorno != null;
        }

        public List<Parametro> RetornaParametrosDoPerfil(int perfilId)
        {
            return new ParametroRepository().RetornaListaDeParametros(perfilId);
        }

        public List<ParametroSimples> RetornaDictionaryParametrosDoPerfil(int perfilId)
        {
            var listaRetorno = new List<ParametroSimples>();

            var listaParametros = new ParametroRepository().RetornaListaDeParametros(perfilId);

            listaParametros.ForEach(x => listaRetorno.Add(new ParametroSimples(x.Campo, x.Valor)));

            return listaRetorno;
        }

        public void SalvaParametro(int perfilId, string campo, string valor)
        {
            var parametro = new Parametro() { PerfilId = perfilId, Campo = campo, Valor = valor };

            new ParametroRepository().SalvaParametro(parametro);
        }

        public bool DeletaPerfil(int perfilId)
        {
            var perfilRepository = new PerfilRepository();

            perfilRepository.DeletaPerfil(perfilId);

            new ParametroRepository().RemoveParametros(perfilId);

            var perfil = perfilRepository.RetornaPerfilPorId(perfilId);

            return perfil == null;
        }
    }
}
