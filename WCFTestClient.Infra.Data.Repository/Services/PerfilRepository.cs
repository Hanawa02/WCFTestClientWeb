using System.Collections.Generic;
using System.Linq;
using WCFWebClient.Dominio.Data;
using WCFWebClient.Infra.Data.Repository.Contexto;
using WCFWebClient.Infra.Data.Repository.Contratos;

namespace WCFWebClient.Infra.Data.Repository.Services
{
    public class PerfilRepository : IPerfilRepository
    {

        public Perfil SalvaPerfil(Perfil perfil)
        {
            using (var contexto = new DataContext())
            {
                if (contexto.Perfil.Where(x => x.Capacidade == perfil.Capacidade && x.Servico == perfil.Servico && x.Descricao == perfil.Descricao).ToList().Count == 0)
                {
                    contexto.Perfil.Add(perfil);
                    contexto.SaveChanges();
                }

                var perfilRetorno = contexto.Perfil.FirstOrDefault(x =>
                    x.Descricao == perfil.Descricao &&
                    x.Capacidade == perfil.Capacidade &&
                    x.Servico == perfil.Servico);

                return perfilRetorno;
            }
        }

        public Perfil RetornaPerfilPorId(int perfilId)
        {
            using (var contexto = new DataContext())
            {
                return contexto.Perfil.Find(perfilId);
                //return contexto.Perfil.Include("Parametro").FirstOrDefault(x => x.PerfilId == perfilId);
            }
        }

        public List<Perfil> RetornaListaDePerfis(string nomeMetodo, string nomeInterface)
        {
            using (var contexto = new DataContext())
            {
                return contexto.Perfil.Where(x => x.Capacidade == nomeMetodo && x.Servico == nomeInterface).ToList();
            }
        }

        public void DeletaPerfil(int perfilId)
        {
            using (var contexto = new DataContext())
            {
                Perfil entityToDelete = contexto.Perfil.Find(perfilId);
                contexto.Perfil.Remove(entityToDelete);
                contexto.SaveChanges();
            }
        }

    }
}
