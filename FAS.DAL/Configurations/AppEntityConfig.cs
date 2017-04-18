using FAS.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FAS.DAL.Configurations
{
    public class AppEntityConfig : EntityTypeConfiguration<AppEntity>
    {
        public AppEntityConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Map(x => x.MapInheritedProperties());
            this.Property(x => x.CreatedOn).IsRequired();
        }
    }
}
