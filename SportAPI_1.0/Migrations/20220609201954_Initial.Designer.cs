﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SportAPI.Models;

namespace SportAPI.Migrations
{
    [DbContext(typeof(SportContext))]
    [Migration("20220609201954_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("SportAPI.Models.User.StatsCategory", b =>
                {
                    b.Property<Guid>("StatsCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("StatsCategoryId");

                    b.ToTable("StatsCategories");
                });

            modelBuilder.Entity("SportAPI.Models.User.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SportAPI.Models.User.UserOption", b =>
                {
                    b.Property<Guid>("UserOptionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserOptionsId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersOptions");
                });

            modelBuilder.Entity("SportAPI.Models.User.UserStat", b =>
                {
                    b.Property<Guid>("UserStatsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<Guid?>("StatsCategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Value")
                        .HasColumnType("integer");

                    b.HasKey("UserStatsId");

                    b.HasIndex("StatsCategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersStats");
                });

            modelBuilder.Entity("SportAPI.Models.Workout.Workout", b =>
                {
                    b.Property<Guid>("WorkoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("About")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("WorkoutId");

                    b.HasIndex("UserId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("SportAPI.Models.Workout.WorkoutExercise.WorkoutExercise", b =>
                {
                    b.Property<Guid?>("WorkoutExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<int?>("Calories")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("Duration")
                        .HasColumnType("integer");

                    b.Property<bool>("IsSet")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("Order")
                        .HasColumnType("integer");

                    b.Property<int?>("Repeats")
                        .HasColumnType("integer");

                    b.Property<int?>("Sets")
                        .HasColumnType("integer");

                    b.Property<int?>("Weight")
                        .HasColumnType("integer");

                    b.Property<Guid?>("WorkoutExerciseCategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("WorkoutId")
                        .HasColumnType("uuid");

                    b.HasKey("WorkoutExerciseId");

                    b.HasIndex("WorkoutExerciseCategoryId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutsExercises");
                });

            modelBuilder.Entity("SportAPI.Models.Workout.WorkoutExercise.WorkoutExerciseCategory", b =>
                {
                    b.Property<Guid?>("WorkoutExerciseCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("WorkoutExerciseCategoryId");

                    b.ToTable("WorkoutsExerciseCategory");
                });

            modelBuilder.Entity("SportAPI.Models.Workout.WorkoutExercise.WorkoutExerciseOption", b =>
                {
                    b.Property<Guid>("WorkoutExerciseOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.Property<Guid?>("WorkoutExerciseId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("WorkoutExerciseId1")
                        .HasColumnType("uuid");

                    b.HasKey("WorkoutExerciseOptionId");

                    b.HasIndex("WorkoutExerciseId");

                    b.HasIndex("WorkoutExerciseId1");

                    b.ToTable("WorkoutsExercisesOptions");
                });

            modelBuilder.Entity("SportAPI.Models.Workout.WorkoutOption", b =>
                {
                    b.Property<Guid>("WorkoutOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.Property<Guid>("WorkoutId")
                        .HasColumnType("uuid");

                    b.HasKey("WorkoutOptionId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutsOptions");
                });

            modelBuilder.Entity("SportAPI.Models.User.UserOption", b =>
                {
                    b.HasOne("SportAPI.Models.User.User", null)
                        .WithMany("Options")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SportAPI.Models.User.UserStat", b =>
                {
                    b.HasOne("SportAPI.Models.User.StatsCategory", null)
                        .WithMany("UsersStatsInCategory")
                        .HasForeignKey("StatsCategoryId");

                    b.HasOne("SportAPI.Models.User.User", null)
                        .WithMany("Stats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportAPI.Models.Workout.Workout", b =>
                {
                    b.HasOne("SportAPI.Models.User.User", null)
                        .WithMany("Workouts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportAPI.Models.Workout.WorkoutExercise.WorkoutExercise", b =>
                {
                    b.HasOne("SportAPI.Models.Workout.WorkoutExercise.WorkoutExerciseCategory", "WorkoutExerciseCategory")
                        .WithMany("WorkoutExercises")
                        .HasForeignKey("WorkoutExerciseCategoryId");

                    b.HasOne("SportAPI.Models.Workout.Workout", null)
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId");

                    b.Navigation("WorkoutExerciseCategory");
                });

            modelBuilder.Entity("SportAPI.Models.Workout.WorkoutExercise.WorkoutExerciseOption", b =>
                {
                    b.HasOne("SportAPI.Models.Workout.WorkoutExercise.WorkoutExercise", "WorkoutExercise")
                        .WithMany("Options")
                        .HasForeignKey("WorkoutExerciseId");

                    b.HasOne("SportAPI.Models.Workout.WorkoutExercise.WorkoutExercise", null)
                        .WithMany()
                        .HasForeignKey("WorkoutExerciseId1");

                    b.Navigation("WorkoutExercise");
                });

            modelBuilder.Entity("SportAPI.Models.Workout.WorkoutOption", b =>
                {
                    b.HasOne("SportAPI.Models.Workout.Workout", null)
                        .WithMany("Options")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportAPI.Models.User.StatsCategory", b =>
                {
                    b.Navigation("UsersStatsInCategory");
                });

            modelBuilder.Entity("SportAPI.Models.User.User", b =>
                {
                    b.Navigation("Options");

                    b.Navigation("Stats");

                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("SportAPI.Models.Workout.Workout", b =>
                {
                    b.Navigation("Exercises");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("SportAPI.Models.Workout.WorkoutExercise.WorkoutExercise", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("SportAPI.Models.Workout.WorkoutExercise.WorkoutExerciseCategory", b =>
                {
                    b.Navigation("WorkoutExercises");
                });
#pragma warning restore 612, 618
        }
    }
}
