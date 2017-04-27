using System.Data.Entity.ModelConfiguration;
using WCFWebClient.Dominio.Data;

namespace WCFWebClient.Infra.Data.Repository.Configuration
{
    public class ParametroEntityConfiguration : EntityTypeConfiguration<Parametro>
    {
        public ParametroEntityConfiguration()
        {
            HasKey(x => x.ParametroId);

            Property(x => x.Campo)
                .HasMaxLength(1000)
                .IsRequired();

            Property(x => x.Valor)
                .HasMaxLength(1000)
                .IsRequired();

            HasRequired(x => x.Perfil);
        }
    }
}
