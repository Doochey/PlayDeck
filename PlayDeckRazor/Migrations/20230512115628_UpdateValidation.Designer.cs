﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlayDeckRazor.Data;

#nullable disable

namespace PlayDeckRazor.Migrations
{
    [DbContext(typeof(PlayDeckRazorContext))]
    [Migration("20230512115628_UpdateValidation")]
    partial class UpdateValidation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PlayDeckRazor.Model.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime?>("CompleteDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeckID")
                        .HasColumnType("int");

                    b.Property<string>("ImageURL")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("LastPlayed")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PlatformID")
                        .HasColumnType("int");

                    b.Property<int?>("PlayStatus")
                        .HasColumnType("int");

                    b.Property<float?>("PlayTime")
                        .HasColumnType("real");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("PlatformID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("PlayDeckRazor.Model.Platform", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Platform");
                });

            modelBuilder.Entity("PlayDeckRazor.Model.Game", b =>
                {
                    b.HasOne("PlayDeckRazor.Model.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformID");

                    b.Navigation("Platform");
                });
#pragma warning restore 612, 618
        }
    }
}
