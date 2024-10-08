﻿// <auto-generated />
using BestDigiSellerApp.Stripe.Data.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BestDigiSellerApp.Stripe.Api.Migrations
{
    [DbContext(typeof(StripeContext))]
    partial class StripeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BestDigiSellerApp.Stripe.Entity.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("CashbackAmount")
                        .HasColumnType("float");

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FiecheNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("BestDigiSellerApp.Stripe.Entity.InvoiceProductDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<double>("ProductAmount")
                        .HasColumnType("float");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductQuantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceProductDetail");
                });

            modelBuilder.Entity("BestDigiSellerApp.Stripe.Entity.InvoiceProductDetail", b =>
                {
                    b.HasOne("BestDigiSellerApp.Stripe.Entity.Invoice", "Invoice")
                        .WithMany("InvoiceProductDetail")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("BestDigiSellerApp.Stripe.Entity.Invoice", b =>
                {
                    b.Navigation("InvoiceProductDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
