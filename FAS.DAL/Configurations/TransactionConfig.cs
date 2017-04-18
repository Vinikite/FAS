using FAS.Domain;
using System.Data.Entity.ModelConfiguration;

namespace FAS.DAL.Configurations
{
    public class TransactionConfig : EntityTypeConfiguration<Transaction>
    {
        public TransactionConfig()
        {
            this.Property(x => x.Comission)
                .IsRequired();

            this.HasRequired(x => x.TransactionType)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.IdTransactionType);
            this.HasRequired(x => x.Score)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.IdScore);
            this.HasRequired(x => x.Category)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.IdCategory);
            this.HasRequired(x => x.Bank)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.IdBank);

            this.Map(x => x.MapInheritedProperties());
            this.ToTable("Transactions");
        }
    }
}
