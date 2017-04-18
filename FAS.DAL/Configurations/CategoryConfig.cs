using FAS.Domain;
using System.Data.Entity.ModelConfiguration;

namespace FAS.DAL.Configurations
{
    public class CategoryConfig : EntityTypeConfiguration<Category>
    {
        public CategoryConfig()
        {
            this.Property(x => x.Name)
                .IsRequired();
            this.Map(m => m.MapInheritedProperties());
            this.ToTable("Categories");
        }
    }
}
