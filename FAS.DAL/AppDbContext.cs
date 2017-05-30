using FAS.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using FAS.Core;
using FAS.DAL.Configurations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAS.DAL
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid, UserLogin, UserRole, UserClaim>
    {
        static AppDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MyGoals> MyGoalss { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<StatusScore> StatusScores { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<TypeScore> TypeScores { get; set; }
        public DbSet<ViewScore> ViewScores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AppEntityConfig());
            modelBuilder.Configurations.Add(new AdressConfig());
            modelBuilder.Configurations.Add(new BankConfig());
            modelBuilder.Configurations.Add(new CategoryConfig());
            modelBuilder.Configurations.Add(new MyGoalsConfig());
            modelBuilder.Configurations.Add(new ScoreConfig());
            modelBuilder.Configurations.Add(new StatusScoreConfig());
            modelBuilder.Configurations.Add(new TransactionConfig());
            modelBuilder.Configurations.Add(new TransactionTypeConfig());
            modelBuilder.Configurations.Add(new TypeScoreConfig());
            modelBuilder.Configurations.Add(new ViewScoreConfig());

            modelBuilder.Entity<User>()
                        .HasKey(x => x.Id);
            modelBuilder.Entity<User>()
                        .Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>()
                        .Property(x => x.AverageIncome)
                        .IsRequired();
            modelBuilder.Entity<User>()
                        .HasOptional(x => x.Address)
                        .WithMany(x => x.Users)
                        .HasForeignKey(x => x.IdAddress);

            modelBuilder.Entity<UserClaim>()
                .Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Role>()
                .Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        public override int SaveChanges()
        {
            TimeStamp();
            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync()
        {
            TimeStamp();
            return await base.SaveChangesAsync();
        }

        private void TimeStamp()
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.Entity is IAppEntity<Guid>))
            {
                IAppEntity<Guid> entity = (IAppEntity<Guid>)item.Entity;

                switch (item.State)
                {
                    case EntityState.Added:
                        entity.CreatedOn = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entity.ModifyOn = DateTime.Now;
                        break;
                }
            }
        }

        public void Rollback()
        {
            ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}