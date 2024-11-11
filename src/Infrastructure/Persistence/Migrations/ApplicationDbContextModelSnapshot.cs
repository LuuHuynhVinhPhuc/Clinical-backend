﻿// <auto-generated />
using System;
using ClinicalBackend.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClinicalBackend.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ClinicalBackend.Domain.Entities.FollowUp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Diagnosis")
                        .HasColumnType("text");

                    b.Property<string>("History")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("uuid");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<string>("Summary")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("FollowUps");
                });

            modelBuilder.Entity("ClinicalBackend.Domain.Entities.Medicine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Company")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Dosage")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nutritional")
                        .HasColumnType("text");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Specialty")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("ClinicalBackend.Domain.Entities.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("CheckStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("DOB")
                        .HasColumnType("date");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("ClinicalBackend.Domain.Entities.Prescription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BillDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FollowUpId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RevisitDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("FollowUpId");

                    b.HasIndex("PatientId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("ClinicalBackend.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ClinicalBackend.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClinicalBackend.Domain.Entities.FollowUp", b =>
                {
                    b.HasOne("ClinicalBackend.Domain.Entities.Patient", "Patient")
                        .WithMany("FollowUps")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("ClinicalBackend.Domain.Entities.Prescription", b =>
                {
                    b.HasOne("ClinicalBackend.Domain.Entities.FollowUp", "FollowUp")
                        .WithMany()
                        .HasForeignKey("FollowUpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClinicalBackend.Domain.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("ClinicalBackend.Domain.Entities.Product", "Products", b1 =>
                        {
                            b1.Property<Guid>("PrescriptionId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("MedicineId")
                                .HasColumnType("uuid");

                            b1.Property<int>("NumberOfDays")
                                .HasColumnType("integer");

                            b1.Property<int>("Quantity")
                                .HasColumnType("integer");

                            b1.HasKey("PrescriptionId", "Id");

                            b1.HasIndex("MedicineId");

                            b1.ToTable("Product");

                            b1.HasOne("ClinicalBackend.Domain.Entities.Medicine", "Medicine")
                                .WithMany()
                                .HasForeignKey("MedicineId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("PrescriptionId");

                            b1.OwnsOne("ClinicalBackend.Domain.Entities.Instructions", "Instructions", b2 =>
                                {
                                    b2.Property<Guid>("ProductPrescriptionId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("ProductId")
                                        .HasColumnType("integer");

                                    b2.Property<string>("Afternoon")
                                        .HasColumnType("text");

                                    b2.Property<string>("Day")
                                        .HasColumnType("text");

                                    b2.Property<string>("Lunch")
                                        .HasColumnType("text");

                                    b2.Property<int?>("NumberOfDays")
                                        .HasColumnType("integer");

                                    b2.HasKey("ProductPrescriptionId", "ProductId");

                                    b2.ToTable("Product");

                                    b2.WithOwner()
                                        .HasForeignKey("ProductPrescriptionId", "ProductId");
                                });

                            b1.Navigation("Instructions")
                                .IsRequired();

                            b1.Navigation("Medicine");
                        });

                    b.Navigation("FollowUp");

                    b.Navigation("Patient");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("ClinicalBackend.Domain.Entities.User", b =>
                {
                    b.HasOne("ClinicalBackend.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ClinicalBackend.Domain.Entities.Patient", b =>
                {
                    b.Navigation("FollowUps");
                });
#pragma warning restore 612, 618
        }
    }
}
