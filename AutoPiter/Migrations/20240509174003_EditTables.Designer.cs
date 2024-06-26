﻿// <auto-generated />
using System;
using AutoPiter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutoPiter.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240509174003_EditTables")]
    partial class EditTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AutoPiter.Domain.Entities.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("AutoPiter.Domain.Entities.BranchAndDevice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("DeviceId");

                    b.ToTable("BranchesAndDevices");
                });

            modelBuilder.Entity("AutoPiter.Domain.Entities.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ConnectionType")
                        .HasColumnType("int");

                    b.Property<string>("MACAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("AutoPiter.Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("AutoPiter.Domain.Entities.PrintJob", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PagesCount")
                        .HasColumnType("int");

                    b.Property<string>("PrintJobName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("PrintJobs");
                });

            modelBuilder.Entity("AutoPiter.Domain.Entities.BranchAndDevice", b =>
                {
                    b.HasOne("AutoPiter.Domain.Entities.Branch", "Branch")
                        .WithMany("BranchAndDevices")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoPiter.Domain.Entities.Device", "Device")
                        .WithMany("BranchAndDevices")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Device");
                });

            modelBuilder.Entity("AutoPiter.Domain.Entities.Employee", b =>
                {
                    b.HasOne("AutoPiter.Domain.Entities.Branch", "Branch")
                        .WithMany("Employees")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("AutoPiter.Domain.Entities.PrintJob", b =>
                {
                    b.HasOne("AutoPiter.Domain.Entities.Device", "Device")
                        .WithMany("PrintJobs")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoPiter.Domain.Entities.Employee", "Employee")
                        .WithMany("PrintJobs")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AutoPiter.Domain.Entities.Branch", b =>
                {
                    b.Navigation("BranchAndDevices");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("AutoPiter.Domain.Entities.Device", b =>
                {
                    b.Navigation("BranchAndDevices");

                    b.Navigation("PrintJobs");
                });

            modelBuilder.Entity("AutoPiter.Domain.Entities.Employee", b =>
                {
                    b.Navigation("PrintJobs");
                });
#pragma warning restore 612, 618
        }
    }
}
