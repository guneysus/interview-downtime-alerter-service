﻿// <auto-generated />
using System;
using DowntimeAlerterWeb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DowntimeAlerterWeb.Migrations.DowntimeAlerterData
{
    [DbContext(typeof(DowntimeAlerterDataContext))]
    partial class DowntimeAlerterDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("DowntimeAlerterWeb.Entities.Monitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Interval")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Monitors");
                });

            modelBuilder.Entity("DowntimeAlerterWeb.Entities.StatusLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExecutedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("HttpStatusCode")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MonitorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MonitorId");

                    b.ToTable("StatusLogs");
                });

            modelBuilder.Entity("DowntimeAlerterWeb.Entities.StatusLog", b =>
                {
                    b.HasOne("DowntimeAlerterWeb.Entities.Monitor", "Monitor")
                        .WithMany()
                        .HasForeignKey("MonitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Monitor");
                });
#pragma warning restore 612, 618
        }
    }
}
