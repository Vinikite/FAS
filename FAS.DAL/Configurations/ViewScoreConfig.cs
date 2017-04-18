using FAS.Domain;
using System.Data.Entity.ModelConfiguration;

namespace FAS.DAL.Configurations
{
    public class ViewScoreConfig : EntityTypeConfiguration<ViewScore>
    {
        public ViewScoreConfig()
        {
            this.Property(x => x.Name)
                .IsRequired();
            this.Map(x => x.MapInheritedProperties());
            this.ToTable("ViewScores");
        }
    }
}
