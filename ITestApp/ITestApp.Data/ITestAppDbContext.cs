using ITestApp.Data.Models;
using ITestApp.Data.Models.Abstracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ITestApp.Data
{
    public class ITestAppDbContext : IdentityDbContext<User>
    {
        public ITestAppDbContext(DbContextOptions<ITestAppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Answer> Answer { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<UserTest> UserTests { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Many to many
            //User to Test
            builder.Entity<UserTest>()
                .HasKey(ut => new { ut.UserId, ut.TestId });

            builder.Entity<UserTest>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTests)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserTest>()
                .HasOne(ut => ut.Test)
                .WithMany(t => t.UserTests)
                .HasForeignKey(ut => ut.TestId)
                .OnDelete(DeleteBehavior.Restrict);

            // One to many
            // Author to test
            builder.Entity<Test>()
               .HasOne(t => t.Author)
               .WithMany(a => a.Tests)
               .OnDelete(DeleteBehavior.Restrict);

            // Test to category
            builder.Entity<Test>()
                    .HasOne(c => c.Category)
                    .WithMany(t => t.Tests)
                    .OnDelete(DeleteBehavior.Restrict);

            // Status to test
            builder.Entity<Test>()
                .HasOne(s => s.Status)
                .WithMany(t => t.Tests)
                .OnDelete(DeleteBehavior.Restrict);

            // Question to test
            builder.Entity<Question>()
                .HasOne(t => t.Test)
                .WithMany(q => q.Questions)
                .OnDelete(DeleteBehavior.Restrict);

            // Question to answer
            builder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
