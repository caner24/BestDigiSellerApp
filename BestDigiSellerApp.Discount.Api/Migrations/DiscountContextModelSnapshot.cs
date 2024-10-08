﻿// <auto-generated />
using System;
using BestDigiSellerApp.Discount.Data.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BestDigiSellerApp.Discount.Api.Migrations
{
    [DbContext(typeof(DiscountContext))]
    partial class DiscountContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BestDigiSellerApp.Discount.Entity.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CouponCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CouponPercentage")
                        .HasColumnType("int");

                    b.Property<int>("DiscountUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpireTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DiscountUserId");

                    b.ToTable("Discount");
                });

            modelBuilder.Entity("BestDigiSellerApp.Discount.Entity.DiscountUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("WasItUsed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("DiscountUser");
                });

            modelBuilder.Entity("BestDigiSellerApp.Discount.Entity.Discount", b =>
                {
                    b.HasOne("BestDigiSellerApp.Discount.Entity.DiscountUser", "DiscountUser")
                        .WithMany("Discounts")
                        .HasForeignKey("DiscountUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiscountUser");
                });

            modelBuilder.Entity("BestDigiSellerApp.Discount.Entity.DiscountUser", b =>
                {
                    b.Navigation("Discounts");
                });
#pragma warning restore 612, 618
        }
    }
}
