using FAS.Domain;
using System.Data.Entity.ModelConfiguration;

namespace FAS.DAL.Configurations
{
    public class AdressConfig : EntityTypeConfiguration<Address>
    {
        public AdressConfig()
        {
            this.Property(x => x.City)
                .IsRequired();
            this.Property(x => x.Country)
                .IsRequired();
            this.Property(x => x.Flat)
                .IsRequired();
            this.Property(x => x.House)
                .IsRequired();
            this.Property(x => x.Street)
                .IsRequired();

            this.Map(m => m.MapInheritedProperties());
            this.ToTable("Addresses");
        }
    }
}
