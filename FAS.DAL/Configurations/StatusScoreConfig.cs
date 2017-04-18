using FAS.Domain;
using System.Data.Entity.ModelConfiguration;

namespace FAS.DAL.Configurations
{
    public class StatusScoreConfig : EntityTypeConfiguration<StatusScore>
    {
        public StatusScoreConfig()
        {
            this.Property(x => x.Name)
                .IsRequired();
            this.Map(x => x.MapInheritedProperties());
            this.ToTable("Statusses");
        }
    }
}
