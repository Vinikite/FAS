using FAS.Domain;
using System.Data.Entity.ModelConfiguration;

namespace FAS.DAL.Configurations
{
    public class BankConfig : EntityTypeConfiguration<Bank>
    {
        public BankConfig()
        {
            this.Property(x => x.Name);
            this.Map(m => m.MapInheritedProperties());
            this.ToTable("Banks");
        }
    }
}
