﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestDataBaseEx.Model;

namespace TestDataBaseEx.Migrations
{
    [DbContext(typeof(TESTDbContext))]
    partial class TESTDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestDataBaseEx.Model.Consultant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConsultantName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Consultant");
                });

            modelBuilder.Entity("TestDataBaseEx.Model.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("CurrentIntent");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("TestDataBaseEx.Model.ReturnVisitTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ActualTime");

                    b.Property<int>("ConsultantId");

                    b.Property<string>("CustomerId")
                        .IsRequired();

                    b.Property<DateTime>("ExpectedTime");

                    b.Property<int>("Intent");

                    b.HasKey("Id");

                    b.ToTable("ReturnVisitTask");
                });

            modelBuilder.Entity("TestDataBaseEx.Model.VisitRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConsultantId");

                    b.Property<string>("CustomerId")
                        .IsRequired();

                    b.Property<int>("Intent");

                    b.Property<DateTime>("VisitTime");

                    b.HasKey("Id");

                    b.ToTable("VisitRecord");
                });
#pragma warning restore 612, 618
        }
    }
}