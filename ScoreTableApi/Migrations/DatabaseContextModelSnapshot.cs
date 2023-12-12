﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScoreTableApi.Data;

#nullable disable

namespace ScoreTableApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameTeam", b =>
                {
                    b.Property<int>("GamesId")
                        .HasColumnType("int");

                    b.Property<int>("TeamsId")
                        .HasColumnType("int");

                    b.HasKey("GamesId", "TeamsId");

                    b.HasIndex("TeamsId");

                    b.ToTable("GameTeam");
                });

            modelBuilder.Entity("PlayerTeam", b =>
                {
                    b.Property<int>("PlayersId")
                        .HasColumnType("int");

                    b.Property<int>("TeamsId")
                        .HasColumnType("int");

                    b.HasKey("PlayersId", "TeamsId");

                    b.HasIndex("TeamsId");

                    b.ToTable("PlayerTeam");
                });

            modelBuilder.Entity("ScoreTableApi.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameFormatId")
                        .HasColumnType("int");

                    b.Property<int>("GameStatusId")
                        .HasColumnType("int");

                    b.Property<int>("PeriodCount")
                        .HasColumnType("int");

                    b.Property<int>("PeriodLength")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameFormatId");

                    b.HasIndex("GameStatusId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("ScoreTableApi.Models.GameFormat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameFormat");
                });

            modelBuilder.Entity("ScoreTableApi.Models.GameStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameStatus");
                });

            modelBuilder.Entity("ScoreTableApi.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("ScoreTableApi.Models.PlayerStatline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Assists")
                        .HasColumnType("int");

                    b.Property<int>("Blocks")
                        .HasColumnType("int");

                    b.Property<int>("Fga")
                        .HasColumnType("int");

                    b.Property<int>("Fgm")
                        .HasColumnType("int");

                    b.Property<int>("Fouls")
                        .HasColumnType("int");

                    b.Property<int>("Fta")
                        .HasColumnType("int");

                    b.Property<int>("Ftm")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<bool>("IsStarter")
                        .HasColumnType("bit");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("Rebounds")
                        .HasColumnType("int");

                    b.Property<int>("Steals")
                        .HasColumnType("int");

                    b.Property<int>("Tpa")
                        .HasColumnType("int");

                    b.Property<int>("Tpm")
                        .HasColumnType("int");

                    b.Property<int>("Turnovers")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerStatlines");
                });

            modelBuilder.Entity("ScoreTableApi.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("GameTeam", b =>
                {
                    b.HasOne("ScoreTableApi.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScoreTableApi.Models.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlayerTeam", b =>
                {
                    b.HasOne("ScoreTableApi.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScoreTableApi.Models.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScoreTableApi.Models.Game", b =>
                {
                    b.HasOne("ScoreTableApi.Models.GameFormat", "GameFormat")
                        .WithMany("Games")
                        .HasForeignKey("GameFormatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScoreTableApi.Models.GameStatus", "GameStatus")
                        .WithMany("Games")
                        .HasForeignKey("GameStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameFormat");

                    b.Navigation("GameStatus");
                });

            modelBuilder.Entity("ScoreTableApi.Models.PlayerStatline", b =>
                {
                    b.HasOne("ScoreTableApi.Models.Game", "Game")
                        .WithMany("PlayerStatlines")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScoreTableApi.Models.Player", "Player")
                        .WithMany("PlayerStatlines")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("ScoreTableApi.Models.Game", b =>
                {
                    b.Navigation("PlayerStatlines");
                });

            modelBuilder.Entity("ScoreTableApi.Models.GameFormat", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("ScoreTableApi.Models.GameStatus", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("ScoreTableApi.Models.Player", b =>
                {
                    b.Navigation("PlayerStatlines");
                });
#pragma warning restore 612, 618
        }
    }
}
