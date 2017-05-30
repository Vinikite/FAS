using FAS.Domain;
using System.Data.Entity.ModelConfiguration;

namespace FAS.DAL.Configurations
{
    public class MyGoalsConfig : EntityTypeConfiguration<MyGoals>
    {
        public MyGoalsConfig()
        {
            this.Property(x => x.Name)
                .IsRequired();
            this.Property(x => x.Price)
                .IsRequired();
            this.Map(m => m.MapInheritedProperties());
            this.ToTable("MyGoals");
        }
    }
}
