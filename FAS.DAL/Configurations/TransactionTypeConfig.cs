using FAS.Domain;
using System.Data.Entity.ModelConfiguration;

namespace FAS.DAL.Configurations
{
    public class TransactionTypeConfig : EntityTypeConfiguration<TransactionType>
    {
        public TransactionTypeConfig()
        {
            this.Property(x => x.Name)
                .IsRequired();
            this.Map(x => x.MapInheritedProperties());
            this.ToTable("TransactionTypes");
        }
    }
}
