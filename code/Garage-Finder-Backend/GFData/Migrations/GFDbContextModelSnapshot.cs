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

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BrandID")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicensePlates")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeCar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("CarID");

                    b.HasIndex("BrandID");

                    b.HasIndex("UserID");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("GFData.Models.Entity.CategoryGarage", b =>
                {
                    b.Property<int>("CategoryGarageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryGarageID"), 1L, 1);

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("GarageID")
                        .HasColumnType("int");

                    b.HasKey("CategoryGarageID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("GarageID");

                    b.ToTable("CategoryGarage");
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

            modelBuilder.Entity("GFData.Models.Entity.FileGuestOrders", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileId"), 1L, 1);

                    b.Property<string>("FileLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GuestOrderID")
                        .HasColumnType("int");

                    b.HasKey("FileId");

                    b.HasIndex("GuestOrderID");

                    b.ToTable("FileGuestOrders");
                });

            modelBuilder.Entity("GFData.Models.Entity.FileOrders", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileId"), 1L, 1);

                    b.Property<string>("FileLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.HasKey("FileId");

                    b.HasIndex("OrderID");

                    b.ToTable("FileOrders");
                });

            modelBuilder.Entity("GFData.Models.Entity.Garage", b =>
                {
                    b.Property<int>("GarageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GarageID"), 1L, 1);

                    b.Property<string>("AddressDetail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CloseTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DistrictsID")
                        .HasColumnType("int");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GarageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("LatAddress")
                        .HasColumnType("float");

                    b.Property<double?>("LngAddress")
                        .HasColumnType("float");

                    b.Property<string>("OpenTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProvinceID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GarageID");

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

            modelBuilder.Entity("GFData.Models.Entity.GuestOrder", b =>
                {
                    b.Property<int>("GuestOrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GuestOrderID"), 1L, 1);

                    b.Property<int?>("BrandCarID")
                        .HasColumnType("int");

                    b.Property<int>("CategoryGarageID")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GFOrderID")
                        .HasColumnType("int");

                    b.Property<int>("GarageID")
                        .HasColumnType("int");

                    b.Property<string>("LicensePlates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeAppointment")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TypeCar")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GuestOrderID");

                    b.HasIndex("BrandCarID");

                    b.HasIndex("CategoryGarageID");

                    b.HasIndex("GFOrderID")
                        .IsUnique();

                    b.HasIndex("GarageID");

                    b.ToTable("GuestOrder");
                });

            modelBuilder.Entity("GFData.Models.Entity.ImageGarage", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageID"), 1L, 1);

                    b.Property<int>("GarageID")
                        .HasColumnType("int");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageID");

                    b.HasIndex("GarageID");

                    b.ToTable("ImageGarage");
                });

            modelBuilder.Entity("GFData.Models.Entity.ImageGuestOrder", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageID"), 1L, 1);

                    b.Property<int>("GuestOrderID")
                        .HasColumnType("int");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageID");

                    b.HasIndex("GuestOrderID");

                    b.ToTable("ImageGuestOrders");
                });

            modelBuilder.Entity("GFData.Models.Entity.ImageOrders", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageID"), 1L, 1);

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.HasKey("ImageID");

                    b.HasIndex("OrderID");

                    b.ToTable("ImageOrders");
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

                    b.Property<int>("CategoryGarageID")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GFOrderID")
                        .HasColumnType("int");

                    b.Property<int>("GarageID")
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

                    b.HasIndex("CategoryGarageID");

                    b.HasIndex("GFOrderID")
                        .IsUnique();

                    b.HasIndex("GarageID");

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

                    b.Property<int>("CategoryGarageID")
                        .HasColumnType("int");

                    b.Property<int?>("CategorysCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Cost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GarageID")
                        .HasColumnType("int");

                    b.Property<string>("NameService")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceID");

                    b.HasIndex("CategoryGarageID");

                    b.HasIndex("CategorysCategoryID");

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

                    b.Property<string>("AddressDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DistrictId")
                        .HasColumnType("int");

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

                    b.Property<int?>("ProvinceId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("GFData.Models.Entity.CategoryGarage", b =>
                {
                    b.HasOne("GFData.Models.Entity.Categorys", "Categorys")
                        .WithMany("CategoryGarages")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Garage", "Garage")
                        .WithMany("CategoryGarages")
                        .HasForeignKey("GarageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorys");

                    b.Navigation("Garage");
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

            modelBuilder.Entity("GFData.Models.Entity.FileGuestOrders", b =>
                {
                    b.HasOne("GFData.Models.Entity.GuestOrder", "Orders")
                        .WithMany("FileOrders")
                        .HasForeignKey("GuestOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("GFData.Models.Entity.FileOrders", b =>
                {
                    b.HasOne("GFData.Models.Entity.Orders", "Orders")
                        .WithMany("FileOrders")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");
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

            modelBuilder.Entity("GFData.Models.Entity.GuestOrder", b =>
                {
                    b.HasOne("GFData.Models.Entity.Brand", "Brand")
                        .WithMany("GuestOrders")
                        .HasForeignKey("BrandCarID");

                    b.HasOne("GFData.Models.Entity.CategoryGarage", "CategoryGarage")
                        .WithMany("GuestOrders")
                        .HasForeignKey("CategoryGarageID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Garage", "Garage")
                        .WithMany()
                        .HasForeignKey("GarageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("CategoryGarage");

                    b.Navigation("Garage");
                });

            modelBuilder.Entity("GFData.Models.Entity.ImageGarage", b =>
                {
                    b.HasOne("GFData.Models.Entity.Garage", "Garage")
                        .WithMany("ImageGarages")
                        .HasForeignKey("GarageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garage");
                });

            modelBuilder.Entity("GFData.Models.Entity.ImageGuestOrder", b =>
                {
                    b.HasOne("GFData.Models.Entity.GuestOrder", "Orders")
                        .WithMany("ImageOrders")
                        .HasForeignKey("GuestOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("GFData.Models.Entity.ImageOrders", b =>
                {
                    b.HasOne("GFData.Models.Entity.Orders", "Orders")
                        .WithMany("ImageOrders")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");
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

                    b.HasOne("GFData.Models.Entity.CategoryGarage", "CategoryGarage")
                        .WithMany("Orders")
                        .HasForeignKey("CategoryGarageID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Garage", "Garage")
                        .WithMany("Orders")
                        .HasForeignKey("GarageID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("CategoryGarage");

                    b.Navigation("Garage");
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
                    b.HasOne("GFData.Models.Entity.CategoryGarage", "CategoryGarage")
                        .WithMany("Services")
                        .HasForeignKey("CategoryGarageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GFData.Models.Entity.Categorys", null)
                        .WithMany("Services")
                        .HasForeignKey("CategorysCategoryID");

                    b.Navigation("CategoryGarage");
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

                    b.Navigation("GuestOrders");
                });

            modelBuilder.Entity("GFData.Models.Entity.Car", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("GFData.Models.Entity.CategoryGarage", b =>
                {
                    b.Navigation("GuestOrders");

                    b.Navigation("Orders");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("GFData.Models.Entity.Categorys", b =>
                {
                    b.Navigation("CategoryGarages");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("GFData.Models.Entity.Garage", b =>
                {
                    b.Navigation("CategoryGarages");

                    b.Navigation("FavoriteList");

                    b.Navigation("Feedbacks");

                    b.Navigation("GarageBrands");

                    b.Navigation("GarageInfos");

                    b.Navigation("ImageGarages");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("GFData.Models.Entity.GuestOrder", b =>
                {
                    b.Navigation("FileOrders");

                    b.Navigation("ImageOrders");
                });

            modelBuilder.Entity("GFData.Models.Entity.Orders", b =>
                {
                    b.Navigation("FileOrders");

                    b.Navigation("ImageOrders");
                });

            modelBuilder.Entity("GFData.Models.Entity.RoleName", b =>
                {
                    b.Navigation("Users");
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

                    b.Navigation("Invoices");

                    b.Navigation("Notifications");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
