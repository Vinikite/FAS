using FAS.Domain;
using System.Data.Entity.ModelConfiguration;

namespace FAS.DAL.Configurations
{
    public class TypeScoreConfig : EntityTypeConfiguration<TypeScore>
    {
        public TypeScoreConfig()
        {
            this.Property(x => x.Name)
                .IsRequired();
            this.Map(x => x.MapInheritedProperties());
            this.ToTable("TypeScores");
        }
    }
}
