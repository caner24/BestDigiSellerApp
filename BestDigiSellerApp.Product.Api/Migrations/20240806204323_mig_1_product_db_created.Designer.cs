﻿// <auto-generated />
using System;
using BestDigiSellerApp.Product.Data.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BestDigiSellerApp.Product.Api.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20240806204323_mig_1_product_db_created")]
    partial class mig_1_product_db_created
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BestDigiSellerApp.Product.Entity.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("BestDigiSellerApp.Product.Entity.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("BestDigiSellerApp.Product.Entity.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("BestDigiSellerApp.Product.Entity.ProductDetail", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("MaxPoint")
                        .HasColumnType("float");

                    b.Property<double>("PointPercentage")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("ProductDetail");
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoriesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("CategoryProduct");
                });

            modelBuilder.Entity("PhotoProduct", b =>
                {
                    b.Property<int>("PhotosId")
                        .HasColumnType("int");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PhotosId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("PhotoProduct");
                });

            modelBuilder.Entity("BestDigiSellerApp.Product.Entity.ProductDetail", b =>
                {
                    b.HasOne("BestDigiSellerApp.Product.Entity.Product", "Product")
                        .WithOne("ProductDetail")
                        .HasForeignKey("BestDigiSellerApp.Product.Entity.ProductDetail", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("BestDigiSellerApp.Product.Entity.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BestDigiSellerApp.Product.Entity.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhotoProduct", b =>
                {
                    b.HasOne("BestDigiSellerApp.Product.Entity.Photo", null)
                        .WithMany()
                        .HasForeignKey("PhotosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BestDigiSellerApp.Product.Entity.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BestDigiSellerApp.Product.Entity.Product", b =>
                {
                    b.Navigation("ProductDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
