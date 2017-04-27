using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using WCFWebClient.Dominio.Data;

namespace WCFWebClient.Infra.Data.Repository.Configuration
{
    public class PerfilEntityConfiguration : EntityTypeConfiguration<Perfil>
    {
        public PerfilEntityConfiguration()
        {
            HasKey(x => x.PerfilId);

            Property(x => x.Descricao)
                .HasMaxLength(50)
                .IsRequired();

            Property(x => x.Capacidade)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Servico)
                .HasMaxLength(100)
                .IsRequired();                                            
        }

    }
}
