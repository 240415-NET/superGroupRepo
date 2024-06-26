﻿// <auto-generated />
using System;
using LibrarySystem.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibrarySystemAPI.Migrations
{
    [DbContext(typeof(LibrarySystemContext))]
    [Migration("20240607190747_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CS_AS")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibrarySystem.API.Models.Book", b =>
                {
                    b.Property<int>("barcode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("barcode"));

                    b.Property<string>("author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("barcode");

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.API.Models.Checkout", b =>
                {
                    b.Property<Guid>("checkoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("checkoutBookbarcode")
                        .HasColumnType("int");

                    b.Property<Guid>("checkoutUseruserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("dueDate")
                        .HasColumnType("date");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("checkoutId");

                    b.HasIndex("checkoutBookbarcode");

                    b.HasIndex("checkoutUseruserId");

                    b.ToTable("Checkouts", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.API.Models.User", b =>
                {
                    b.Property<Guid>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("LibrarySystem.API.Models.Checkout", b =>
                {
                    b.HasOne("LibrarySystem.API.Models.Book", "checkoutBook")
                        .WithMany("bookCheckouts")
                        .HasForeignKey("checkoutBookbarcode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibrarySystem.API.Models.User", "checkoutUser")
                        .WithMany("userCheckouts")
                        .HasForeignKey("checkoutUseruserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("checkoutBook");

                    b.Navigation("checkoutUser");
                });

            modelBuilder.Entity("LibrarySystem.API.Models.Book", b =>
                {
                    b.Navigation("bookCheckouts");
                });

            modelBuilder.Entity("LibrarySystem.API.Models.User", b =>
                {
                    b.Navigation("userCheckouts");
                });
#pragma warning restore 612, 618
        }
    }
}
