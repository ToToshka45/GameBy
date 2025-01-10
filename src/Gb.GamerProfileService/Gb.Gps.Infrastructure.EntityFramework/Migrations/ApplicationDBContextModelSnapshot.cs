﻿// <auto-generated />
using System;
using GameBy.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gb.Gps.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AboutCondition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AboutReward")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RankId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RankId");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("Domain.Entities.Gamer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AboutMe")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("ContactMe")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("Date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("RankId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RankId");

                    b.ToTable("Gamers");
                });

            modelBuilder.Entity("Domain.Entities.GamerAchievement", b =>
                {
                    b.Property<int>("GamerId")
                        .HasColumnType("integer");

                    b.Property<int>("AchievementId")
                        .HasColumnType("integer");

                    b.HasKey("GamerId", "AchievementId");

                    b.HasIndex("AchievementId");

                    b.ToTable("GamerAchievement");
                });

            modelBuilder.Entity("Domain.Entities.Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("Domain.Entities.Achievement", b =>
                {
                    b.HasOne("Domain.Entities.Rank", "Rank")
                        .WithMany()
                        .HasForeignKey("RankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rank");
                });

            modelBuilder.Entity("Domain.Entities.Gamer", b =>
                {
                    b.HasOne("Domain.Entities.Rank", "Rank")
                        .WithMany("Gamers")
                        .HasForeignKey("RankId");

                    b.Navigation("Rank");
                });

            modelBuilder.Entity("Domain.Entities.GamerAchievement", b =>
                {
                    b.HasOne("Domain.Entities.Achievement", "Achievement")
                        .WithMany("GamerAchievements")
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Gamer", "Gamer")
                        .WithMany("GamerAchievements")
                        .HasForeignKey("GamerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievement");

                    b.Navigation("Gamer");
                });

            modelBuilder.Entity("Domain.Entities.Achievement", b =>
                {
                    b.Navigation("GamerAchievements");
                });

            modelBuilder.Entity("Domain.Entities.Gamer", b =>
                {
                    b.Navigation("GamerAchievements");
                });

            modelBuilder.Entity("Domain.Entities.Rank", b =>
                {
                    b.Navigation("Gamers");
                });
#pragma warning restore 612, 618
        }
    }
}
