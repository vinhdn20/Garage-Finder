﻿// <auto-generated />
using System;
using GFData.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GFData.Migrations
{
    [DbContext(typeof(GFDbContext))]
    partial class GFDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GFData.Models.Entity.Brand", b =>
                {
                    b.Property<int>("BrandID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandID"), 1L, 1);

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandID");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("GFData.Models.Entity.Car", b =>
                {
                    b.Property<int>("CarID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarID"), 1L, 1);

                    b.Property<int>("BrandID")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicensePlates")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LinkImages")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeCar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("CarID");

                    b.HasIndex("BrandID");

                    b.HasIndex("LicensePlates")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("GFData.Models.Entity.Categorys", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("GFData.Models.Entity.FavoriteList", b =>
                {
                    b.Property<int>("FavoriteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavoriteID"), 1L, 1);

                    b.Property<int>("GarageID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("FavoriteID");

                    b.HasIndex("GarageID");

                    b.HasIndex("UserID");

                    b.ToTable("FavoriteList");
                });

            modelBuilder.Entity("GFData.Models.Entity.Feedback", b =>
                {
                    b.Property<int>("FeedbackID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackID"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GarageID")
                        .HasColumnType("int");

                    b.Property<int>("Star")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("FeedbackID");

                    b.HasIndex("GarageID");

                    b.HasIndex("UserID");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("GFData.Models.Entity.Garage", b =>
                {
                    b.Property<int>("GarageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GarageID"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("CloseTime")
                        .HasColumnType("float");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GarageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imagies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("LatAddress")
                        .HasColumnType("float");

                    b.Property<double?>("LngAddress")
                        .HasColumnType("float");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("OpenTime")
                        .HasColumnType("float");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("GarageID");

                    b.HasIndex("UserID");

                    b.HasIndex("PhoneNumber", "EmailAddress")
                        .IsUnique();

                    b.ToTable("Garage");
                });

            modelBuilder.Entity("GFData.Models.Entity.GarageBrand", b =>
                {
                    b.Property<int>("BrID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrID"), 1L, 1);

                    b.Property<int>("BrandID")
                        .HasColumnType("int");

                    b.Property<int>("GarageID")
                        .HasColumnType("int");

                    b.HasKey("BrID");

                    b.HasIndex("BrandID");

                    b.HasIndex("GarageID");

                    b.ToTable("GarageBrand");
                });

            modelBuilder.Entity("GFData.Models.Entity.GarageInfo", b =>
                {
                    b.Property<int>("InfoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InfoID"), 1L, 1);

                    b.Property<int>("GarageID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("InfoID");

                    b.HasIndex("GarageID");

                    b.HasIndex("UserID");

                    b.ToTable("GarageInfo");
                });

            modelBuilder.Entity("GFData.Models.Entity.Invoices", b =>
                {
                    b.Property<int>("InvoicesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoicesID"), 1L, 1);

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubscribeID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserID")
                        .HasColumnType("int");

                    b.HasKey("InvoicesID");

                    b.HasIndex("SubscribeID");

                    b.HasIndex("UsersUserID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("GFData.Models.Entity.Notification", b =>
                {
                    b.Property<int>("NotificationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationID"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("NotificationID");

                    b.HasIndex("UserID");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("GFData.Models.Entity.Orders", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"), 1L, 1);

                    b.Property<int>("CarID")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GarageID")
                        .HasColumnType("int");

                    b.Property<string>("ImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServiceID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeAppointment")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeUpdate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderID");

                    b.HasIndex("CarID");

                    b.HasIndex("GarageID");

                    b.HasIndex("ServiceID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("GFData.Models.Entity.RefreshToken", b =>
                {
                    b.Property<int>("TokenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TokenID"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("TokenID");

                    b.HasIndex("UserID");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("GFData.Models.Entity.RoleName", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"), 1L, 1);

                    b.Property<string>("NameRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("RoleName");
                });

            modelBuilder.Entity("GFData.Models.Entity.Service", b =>
                {
                    b.Property<int>("ServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceID"), 1L, 1);

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("GarageID")
                        .HasColumnType("int");

                    b.Property<string>("NameService")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("GarageID");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("GFData.Models.Entity.Subscribe", b =>
                {
                    b.Property<int>("SubscribeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscribeID"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("SubscribeID");

                    b.ToTable("Subscribe");
                });

            modelBuilder.Entity("GFData.Models.Entity.Users", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LinkImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.HasIndex("PhoneNumber", "EmailAddress")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GFData.Models.Entity.Car", b =>
                {
                    b.HasOne("GFData.Models.Entity.Brand", "Brand")
                        .WithMany("Cars")
                        .HasForeignKey("BrandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Users", "User")
                        .WithMany("Cars")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GFData.Models.Entity.FavoriteList", b =>
                {
                    b.HasOne("GFData.Models.Entity.Garage", "Garage")
                        .WithMany("FavoriteList")
                        .HasForeignKey("GarageID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Users", "User")
                        .WithMany("FavoriteList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garage");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GFData.Models.Entity.Feedback", b =>
                {
                    b.HasOne("GFData.Models.Entity.Garage", null)
                        .WithMany("Feedbacks")
                        .HasForeignKey("GarageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Users", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GFData.Models.Entity.Garage", b =>
                {
                    b.HasOne("GFData.Models.Entity.Users", "User")
                        .WithMany("Garages")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GFData.Models.Entity.GarageBrand", b =>
                {
                    b.HasOne("GFData.Models.Entity.Brand", "Brand")
                        .WithMany("GarageBrands")
                        .HasForeignKey("BrandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Garage", "Garage")
                        .WithMany("GarageBrands")
                        .HasForeignKey("GarageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Garage");
                });

            modelBuilder.Entity("GFData.Models.Entity.GarageInfo", b =>
                {
                    b.HasOne("GFData.Models.Entity.Garage", "Garage")
                        .WithMany("GarageInfos")
                        .HasForeignKey("GarageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Users", "User")
                        .WithMany("GarageInfos")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Garage");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GFData.Models.Entity.Invoices", b =>
                {
                    b.HasOne("GFData.Models.Entity.Subscribe", "Subscribe")
                        .WithMany("Invoices")
                        .HasForeignKey("SubscribeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Users", "Users")
                        .WithMany("Invoices")
                        .HasForeignKey("UsersUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscribe");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("GFData.Models.Entity.Notification", b =>
                {
                    b.HasOne("GFData.Models.Entity.Users", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GFData.Models.Entity.Orders", b =>
                {
                    b.HasOne("GFData.Models.Entity.Car", "Car")
                        .WithMany("Orders")
                        .HasForeignKey("CarID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Garage", "Garage")
                        .WithMany("Orders")
                        .HasForeignKey("GarageID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Service", "Service")
                        .WithMany("Orders")
                        .HasForeignKey("ServiceID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Garage");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("GFData.Models.Entity.RefreshToken", b =>
                {
                    b.HasOne("GFData.Models.Entity.Users", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GFData.Models.Entity.Service", b =>
                {
                    b.HasOne("GFData.Models.Entity.Categorys", "Category")
                        .WithMany("Services")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Garage", null)
                        .WithMany("Services")
                        .HasForeignKey("GarageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("GFData.Models.Entity.Users", b =>
                {
                    b.HasOne("GFData.Models.Entity.RoleName", "RoleName")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoleName");
                });

            modelBuilder.Entity("GFData.Models.Entity.Brand", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("GarageBrands");
                });

            modelBuilder.Entity("GFData.Models.Entity.Car", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("GFData.Models.Entity.Categorys", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("GFData.Models.Entity.Garage", b =>
                {
                    b.Navigation("FavoriteList");

                    b.Navigation("Feedbacks");

                    b.Navigation("GarageBrands");

                    b.Navigation("GarageInfos");

                    b.Navigation("Orders");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("GFData.Models.Entity.RoleName", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("GFData.Models.Entity.Service", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("GFData.Models.Entity.Subscribe", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("GFData.Models.Entity.Users", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("FavoriteList");

                    b.Navigation("Feedbacks");

                    b.Navigation("GarageInfos");

                    b.Navigation("Garages");

                    b.Navigation("Invoices");

                    b.Navigation("Notifications");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
