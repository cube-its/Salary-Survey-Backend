using System;
using Codex.SalarySurvey.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Codex.SalarySurvey.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSurvey> UserSurveys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Text).IsRequired();
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.HasIndex(e => e.SurveyQuestionId)
                    .HasName("IX_Answers_SurveyQuestionId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Answers_UserId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.SurveyQuestion)
                    .WithMany(p => p.QuestionAnswers)
                    .HasForeignKey(d => d.SurveyQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_SurveyQuestions");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QuestionAnswers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_Users");
            });

            modelBuilder.Entity<QuestionOption>(entity =>
            {
                entity.HasIndex(e => e.QuestionId);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionOptions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionOptions_Questions");
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EndsOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.StartsOn).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SurveyQuestion>(entity =>
            {
                entity.HasIndex(e => e.QuestionId);

                entity.HasIndex(e => e.SurveyId);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.SurveyQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyQuestions_Questions");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyQuestions)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyQuestions_Surveys");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .IsUnique();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(78);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.SmsCode).HasMaxLength(20);

                entity.Property(e => e.SmsCodeExpiredOn).HasColumnType("datetime");

                entity.Property(e => e.SmsCodePassedOn).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<UserSurvey>(entity =>
            {
                entity.HasIndex(e => e.SurveyId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.CompletedOn).HasColumnType("datetime");

                entity.Property(e => e.StartedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.UserSurveys)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSurveys_Surveys");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSurveys)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSurveys_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
