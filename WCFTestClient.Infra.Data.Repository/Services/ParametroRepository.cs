using System.Collections.Generic;
using System.Linq;
using WCFWebClient.Dominio.Data;
using WCFWebClient.Infra.Data.Repository.Contexto;
using WCFWebClient.Infra.Data.Repository.Contratos;

namespace WCFWebClient.Infra.Data.Repository.Services
{
    public class ParametroRepository : IParametroRepository
    {
        public void SalvaParametro(Parametro parametro)
        {
            using (var contexto = new DataContext())
            {
                contexto.Parametro.Add(parametro);

                contexto.SaveChanges();
            }
        }

        public List<Parametro> RetornaListaDeParametros(int perfilId)
        {
            using (var contexto = new DataContext())
            {
                return contexto.Parametro.Where(x => x.PerfilId == perfilId).ToList();
            }
        }

        public void RemoveParametros(int perfilId)
        {
            using (var contexto = new DataContext())
            {
                var listaParametrosAntigo = contexto.Parametro.Where(x => x.PerfilId == perfilId).ToList();

                if (listaParametrosAntigo.Count != 0)
                {
                    contexto.Parametro.RemoveRange(listaParametrosAntigo);
                    contexto.SaveChanges();
                }
            }
        }
    }
}
