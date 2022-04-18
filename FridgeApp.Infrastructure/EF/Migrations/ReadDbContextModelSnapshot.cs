﻿// <auto-generated />
using System;
using FridgeApp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FridgeApp.Infrastructure.EF.Migrations
{
    [DbContext(typeof(ReadDbContext))]
    partial class ReadDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("fridges")
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FridgeApp.Infrastructure.EF.Models.FridgeModelReadModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("FridgeModels", "fridges");
                });

            modelBuilder.Entity("FridgeApp.Infrastructure.EF.Models.FridgeProductsReadModel", b =>
                {
                    b.Property<Guid>("FridgeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("FridgeId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("FridgeProducts", "fridges");
                });

            modelBuilder.Entity("FridgeApp.Infrastructure.EF.Models.FridgeReadModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FridgeModelId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("OwnerName")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FridgeModelId");

                    b.ToTable("Fridges", "fridges");
                });

            modelBuilder.Entity("FridgeApp.Infrastructure.EF.Models.ProductReadModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DefaultQuantity")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Products", "fridges");
                });

            modelBuilder.Entity("FridgeApp.Infrastructure.EF.Models.FridgeProductsReadModel", b =>
                {
                    b.HasOne("FridgeApp.Infrastructure.EF.Models.FridgeReadModel", "Fridge")
                        .WithMany("Products")
                        .HasForeignKey("FridgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FridgeApp.Infrastructure.EF.Models.ProductReadModel", "Product")
                        .WithMany("FridgeProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fridge");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FridgeApp.Infrastructure.EF.Models.FridgeReadModel", b =>
                {
                    b.HasOne("FridgeApp.Infrastructure.EF.Models.FridgeModelReadModel", "FridgeModel")
                        .WithMany("Fridges")
                        .HasForeignKey("FridgeModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FridgeModel");
                });

            modelBuilder.Entity("FridgeApp.Infrastructure.EF.Models.FridgeModelReadModel", b =>
                {
                    b.Navigation("Fridges");
                });

            modelBuilder.Entity("FridgeApp.Infrastructure.EF.Models.FridgeReadModel", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("FridgeApp.Infrastructure.EF.Models.ProductReadModel", b =>
                {
                    b.Navigation("FridgeProducts");
                });
#pragma warning restore 612, 618
        }
    }
}