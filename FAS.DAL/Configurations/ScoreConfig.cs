using FAS.Domain;
using System.Data.Entity.ModelConfiguration;

namespace FAS.DAL.Configurations
{
    public class ScoreConfig : EntityTypeConfiguration<Score>
    {
        public ScoreConfig()
        {
            this.Property(x => x.Balance)
                .IsRequired();

            this.HasRequired(x => x.StatusScore)
                .WithMany(x => x.Scores)
                .HasForeignKey(x => x.IdStatus);
            this.HasRequired(x => x.TypeScore)
                .WithMany(x => x.Scores)
                .HasForeignKey(x => x.IdTypeScore);
            this.HasRequired(x => x.User)
                .WithMany(x => x.Scores)
                .HasForeignKey(x => x.IdUser);
            this.HasRequired(x => x.ViewScore)
                .WithMany(x => x.Scores)
                .HasForeignKey(x => x.IdViewScore);

            this.Map(x => x.MapInheritedProperties());
            this.ToTable("Scores");
        }
    }
}
