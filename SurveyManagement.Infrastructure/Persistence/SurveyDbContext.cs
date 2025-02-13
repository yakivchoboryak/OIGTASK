using Microsoft.EntityFrameworkCore;
using SurveyManagement.Domain.Entities;

namespace SurveyManagement.Infrastructure.Persistence
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options) { }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyResult> SurveyResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Alice Johnson", Email = "alice@example.com" },
                new User { Id = 2, Name = "Bob Smith", Email = "bob@example.com" }
            );

            modelBuilder.Entity<Survey>().HasData(
                new Survey { Id = 1, Name = "Customer Satisfaction Survey", CreatedById = 1 },
                new Survey { Id = 2, Name = "Employee Engagement Survey", CreatedById = 2 }
            );

            modelBuilder.Entity<SurveyQuestion>().HasData(
                new SurveyQuestion { Id = 1, SurveyId = 1, QuestionType = 0, QuestionText = "How satisfied are you with our service?" },
                new SurveyQuestion { Id = 2, SurveyId = 1, QuestionType = 0, QuestionText = "Would you recommend us to a friend?" },
                new SurveyQuestion { Id = 3, SurveyId = 2, QuestionType = 0, QuestionText = "How satisfied are you with your work environment?" }
            );

            modelBuilder.Entity<SurveyResult>().HasData(
                new SurveyResult { Id = 1, SurveyId = 1, UserId = 1, Answer = "Very Satisfied" },
                new SurveyResult { Id = 2, SurveyId = 1, UserId = 2, Answer = "Satisfied" },
                new SurveyResult { Id = 3, SurveyId = 2, UserId = 1, Answer = "Very Happy" }
            );

            modelBuilder.Entity<Survey>()
                .HasOne(s => s.CreatedBy)
                .WithMany(u => u.CreatedSurveys)
                .HasForeignKey(s => s.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SurveyQuestion>()
                .HasOne(q => q.Survey)
                .WithMany(s => s.SurveyQuestions)
                .HasForeignKey(q => q.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
