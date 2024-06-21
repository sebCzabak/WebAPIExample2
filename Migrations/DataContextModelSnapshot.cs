﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPIExample2.Data;

#nullable disable

namespace WebAPIExample2.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebAPIExample2.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<bool>("Complaint")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MechanicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("WebAPIExample2.Models.Part", b =>
                {
                    b.Property<int>("PartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PartId"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PartId");

                    b.ToTable("Part");

                    b.HasData(
                        new
                        {
                            PartId = 1,
                            Amount = 40,
                            Name = "Opony"
                        },
                        new
                        {
                            PartId = 2,
                            Amount = 100,
                            Name = "Filtr powietrza"
                        },
                        new
                        {
                            PartId = 3,
                            Amount = 25,
                            Name = "Olej silnikowy"
                        },
                        new
                        {
                            PartId = 4,
                            Amount = 10,
                            Name = "Misa olejowa"
                        },
                        new
                        {
                            PartId = 5,
                            Amount = 30,
                            Name = "Hamulce"
                        });
                });

            modelBuilder.Entity("WebAPIExample2.Models.PartRequest", b =>
                {
                    b.Property<int>("PartRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PartRequestId"));

                    b.Property<string>("MechanicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parts")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PartRequestId");

                    b.ToTable("PartRequest");
                });

            modelBuilder.Entity("WebAPIExample2.Models.RequestedPart", b =>
                {
                    b.Property<int>("RequestedPartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestedPartId"));

                    b.Property<int>("PartId")
                        .HasColumnType("int");

                    b.Property<int>("PartRequestId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("RequestedPartId");

                    b.HasIndex("PartId");

                    b.HasIndex("PartRequestId");

                    b.ToTable("RequestRequestedPart");
                });

            modelBuilder.Entity("WebAPIExample2.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceId");

                    b.ToTable("Service");

                    b.HasData(
                        new
                        {
                            ServiceId = 1,
                            Description = "Szybka wymiana oleju",
                            Price = 100f,
                            ServiceName = "Wymiana oleju"
                        },
                        new
                        {
                            ServiceId = 2,
                            Description = "Zmienimy opony z letnich na zimowe lub z zimowych na letnie",
                            Price = 75f,
                            ServiceName = "Wymiana opon"
                        },
                        new
                        {
                            ServiceId = 3,
                            Description = "Sprawdzimy auto przed zbliżającym się przeglądem",
                            Price = 90f,
                            ServiceName = "Przygotowanie do przeglądu"
                        });
                });

            modelBuilder.Entity("WebAPIExample2.Models.ServiceOrder", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceOrder");
                });

            modelBuilder.Entity("WebAPIExample2.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Forename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "admin@example.com",
                            Forename = "Admin",
                            Password = "password",
                            Role = "admin",
                            Surname = "Admin"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "mechanic@example.com",
                            Forename = "Mechanic",
                            Password = "password",
                            Role = "mechanic",
                            Surname = "Mechanic"
                        },
                        new
                        {
                            UserId = 3,
                            Email = "warehouse@example.com",
                            Forename = "Warehouse",
                            Password = "password",
                            Role = "warehouse",
                            Surname = "Warehouse"
                        });
                });

            modelBuilder.Entity("WebAPIExample2.Models.Order", b =>
                {
                    b.HasOne("WebAPIExample2.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAPIExample2.Models.RequestedPart", b =>
                {
                    b.HasOne("WebAPIExample2.Models.Part", "Part")
                        .WithMany()
                        .HasForeignKey("PartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPIExample2.Models.PartRequest", "PartRequest")
                        .WithMany("RequestedParts")
                        .HasForeignKey("PartRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Part");

                    b.Navigation("PartRequest");
                });

            modelBuilder.Entity("WebAPIExample2.Models.ServiceOrder", b =>
                {
                    b.HasOne("WebAPIExample2.Models.Order", "Order")
                        .WithMany("ServiceOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPIExample2.Models.Service", "Service")
                        .WithMany("ServiceOrders")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("WebAPIExample2.Models.Order", b =>
                {
                    b.Navigation("ServiceOrders");
                });

            modelBuilder.Entity("WebAPIExample2.Models.PartRequest", b =>
                {
                    b.Navigation("RequestedParts");
                });

            modelBuilder.Entity("WebAPIExample2.Models.Service", b =>
                {
                    b.Navigation("ServiceOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
