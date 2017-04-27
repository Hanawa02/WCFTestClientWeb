using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WCFWebClient.Dominio.Data;
using WCFWebClient.Infra.Data.Repository.Configuration;

namespace WCFWebClient.Infra.Data.Repository.Contexto
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("WCFWebClient")
        {
        }

        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Parametro> Parametro { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new PerfilEntityConfiguration());
            modelBuilder.Configurations.Add(new ParametroEntityConfiguration());
        }
    }
}
