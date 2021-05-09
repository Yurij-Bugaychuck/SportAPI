﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportAPI;

namespace SportAPI.Migrations
{
    [DbContext(typeof(SportContext))]
    partial class SportContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SportAPI.Models.StatsCategory", b =>
                {
                    b.Property<Guid>("StatsCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatsCategoryId");

                    b.ToTable("StatsCategories");
                });

            modelBuilder.Entity("SportAPI.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("Phone")
                        .IsUnique()
                        .HasFilter("[Phone] IS NOT NULL");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SportAPI.Models.UserOption", b =>
                {
                    b.Property<Guid>("UserOptionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserOptionsId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersOptions");
                });

            modelBuilder.Entity("SportAPI.Models.UserStat", b =>
                {
                    b.Property<Guid>("UserStatsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("StatsCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Value")
                        .HasColumnType("int");

                    b.HasKey("UserStatsId");

                    b.HasIndex("StatsCategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersStats");
                });

            modelBuilder.Entity("SportAPI.Models.Workout", b =>
                {
                    b.Property<Guid>("WorkoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("WorkoutId");

                    b.HasIndex("UserId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("SportAPI.Models.WorkoutExcercise", b =>
                {
                    b.Property<Guid>("WorkoutExcerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsSet")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Order")
                        .HasColumnType("bigint");

                    b.Property<Guid>("WorkoutId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("WorkoutExcerciseId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutsExcercises");
                });

            modelBuilder.Entity("SportAPI.Models.WorkoutExcerciseCategories", b =>
                {
                    b.Property<Guid>("Workout_excercise_categories_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Workout_excercise_categories_id");

                    b.ToTable("WorkoutsExcercisesCategories");
                });

            modelBuilder.Entity("SportAPI.Models.WorkoutExcerciseCategory", b =>
                {
                    b.Property<Guid>("WorkoutExcerciseEategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<Guid>("WorkoutExcerciseCategoriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkoutExcerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("WorkoutExcerciseEategoryId");

                    b.HasIndex("WorkoutExcerciseCategoriesId");

                    b.HasIndex("WorkoutExcerciseId");

                    b.ToTable("WorkoutsExcercisesCategory");
                });

            modelBuilder.Entity("SportAPI.Models.WorkoutExcerciseOption", b =>
                {
                    b.Property<Guid>("WorkoutExcerciseOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<Guid?>("WorkoutExcerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WorkoutExcerciseId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("WorkoutExcerciseOptionId");

                    b.HasIndex("WorkoutExcerciseId");

                    b.HasIndex("WorkoutExcerciseId1");

                    b.ToTable("WorkoutsExcercisesOptions");
                });

            modelBuilder.Entity("SportAPI.Models.WorkoutOption", b =>
                {
                    b.Property<Guid>("WorkoutOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<Guid>("WorkoutId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("WorkoutOptionId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutsOptions");
                });

            modelBuilder.Entity("SportAPI.Models.UserOption", b =>
                {
                    b.HasOne("SportAPI.Models.User", null)
                        .WithMany("Options")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SportAPI.Models.UserStat", b =>
                {
                    b.HasOne("SportAPI.Models.StatsCategory", null)
                        .WithMany("UsersStatsInCategory")
                        .HasForeignKey("StatsCategoryId");

                    b.HasOne("SportAPI.Models.User", null)
                        .WithMany("Stats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportAPI.Models.Workout", b =>
                {
                    b.HasOne("SportAPI.Models.User", null)
                        .WithMany("Workouts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportAPI.Models.WorkoutExcercise", b =>
                {
                    b.HasOne("SportAPI.Models.Workout", null)
                        .WithMany("Excercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportAPI.Models.WorkoutExcerciseCategory", b =>
                {
                    b.HasOne("SportAPI.Models.WorkoutExcerciseCategories", "WorkoutExcerciseCategories")
                        .WithMany()
                        .HasForeignKey("WorkoutExcerciseCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportAPI.Models.WorkoutExcercise", "WorkoutExcercise")
                        .WithMany("Categories")
                        .HasForeignKey("WorkoutExcerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkoutExcercise");

                    b.Navigation("WorkoutExcerciseCategories");
                });

            modelBuilder.Entity("SportAPI.Models.WorkoutExcerciseOption", b =>
                {
                    b.HasOne("SportAPI.Models.WorkoutExcercise", "WorkoutExcercise")
                        .WithMany("Options")
                        .HasForeignKey("WorkoutExcerciseId");

                    b.HasOne("SportAPI.Models.WorkoutExcercise", null)
                        .WithMany()
                        .HasForeignKey("WorkoutExcerciseId1");

                    b.Navigation("WorkoutExcercise");
                });

            modelBuilder.Entity("SportAPI.Models.WorkoutOption", b =>
                {
                    b.HasOne("SportAPI.Models.Workout", null)
                        .WithMany("Options")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportAPI.Models.StatsCategory", b =>
                {
                    b.Navigation("UsersStatsInCategory");
                });

            modelBuilder.Entity("SportAPI.Models.User", b =>
                {
                    b.Navigation("Options");

                    b.Navigation("Stats");

                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("SportAPI.Models.Workout", b =>
                {
                    b.Navigation("Excercises");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("SportAPI.Models.WorkoutExcercise", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
