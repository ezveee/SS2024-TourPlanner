﻿// <auto-generated />
using System;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(TourPlannerContext))]
    partial class TourPlannerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Models.Tour", b =>
                {
                    b.Property<int>("TourId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("tourid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TourId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<float>("Distance")
                        .HasColumnType("real")
                        .HasColumnName("distance");

                    b.Property<double>("EstimatedTime")
                        .HasColumnType("double precision")
                        .HasColumnName("estimatedtime");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("from");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("RouteInformation")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("routeinformation");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("to");

                    b.Property<string>("TransportType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("transporttype");

                    b.HasKey("TourId");

                    b.ToTable("tours");
                });

            modelBuilder.Entity("DataAccess.Models.TourLog", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("logid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LogId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer")
                        .HasColumnName("difficulty");

                    b.Property<float>("Distance")
                        .HasColumnType("real")
                        .HasColumnName("distance");

                    b.Property<int>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<double>("Time")
                        .HasColumnType("double precision")
                        .HasColumnName("time");

                    b.Property<int>("TourId")
                        .HasColumnType("integer")
                        .HasColumnName("tourid");

                    b.HasKey("LogId");

                    b.HasIndex("TourId");

                    b.ToTable("tourlogs");
                });

            modelBuilder.Entity("DataAccess.Models.TourLog", b =>
                {
                    b.HasOne("DataAccess.Models.Tour", "Tour")
                        .WithMany("TourLogs")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("DataAccess.Models.Tour", b =>
                {
                    b.Navigation("TourLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
