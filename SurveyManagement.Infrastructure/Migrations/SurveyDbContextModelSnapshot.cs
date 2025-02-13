using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyManagement.Infrastructure.Persistence;

#nullable disable

namespace SurveyManagement.Infrastructure.Migrations
{
    //add Constraint for start/end date
    [DbContext(typeof(SurveyDbContext))]
    partial class SurveyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SurveyManagement.Domain.Entities.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Surveys");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedById = 1,
                            EndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Customer Satisfaction Survey",
                            StartDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            CreatedById = 2,
                            EndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Employee Engagement Survey",
                            StartDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 0
                        });
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.SurveyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionType")
                        .HasColumnType("int");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyQuestions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            QuestionText = "How satisfied are you with our service?",
                            QuestionType = 0,
                            SurveyId = 1
                        },
                        new
                        {
                            Id = 2,
                            QuestionText = "Would you recommend us to a friend?",
                            QuestionType = 0,
                            SurveyId = 1
                        },
                        new
                        {
                            Id = 3,
                            QuestionText = "How satisfied are you with your work environment?",
                            QuestionType = 0,
                            SurveyId = 2
                        });
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.SurveyResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.HasIndex("UserId");

                    b.ToTable("SurveyResults");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Answer = "Very Satisfied",
                            SubmittedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SurveyId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Answer = "Satisfied",
                            SubmittedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SurveyId = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            Answer = "Very Happy",
                            SubmittedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SurveyId = 2,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "alice@example.com",
                            Name = "Alice Johnson"
                        },
                        new
                        {
                            Id = 2,
                            Email = "bob@example.com",
                            Name = "Bob Smith"
                        });
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.Survey", b =>
                {
                    b.HasOne("SurveyManagement.Domain.Entities.User", "CreatedBy")
                        .WithMany("CreatedSurveys")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.SurveyQuestion", b =>
                {
                    b.HasOne("SurveyManagement.Domain.Entities.Survey", "Survey")
                        .WithMany("SurveyQuestions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.SurveyResult", b =>
                {
                    b.HasOne("SurveyManagement.Domain.Entities.Survey", "Survey")
                        .WithMany("SurveyResults")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SurveyManagement.Domain.Entities.User", "User")
                        .WithMany("SurveyResults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.Survey", b =>
                {
                    b.Navigation("SurveyQuestions");

                    b.Navigation("SurveyResults");
                });

            modelBuilder.Entity("SurveyManagement.Domain.Entities.User", b =>
                {
                    b.Navigation("CreatedSurveys");

                    b.Navigation("SurveyResults");
                });
#pragma warning restore 612, 618
        }
    }
}
