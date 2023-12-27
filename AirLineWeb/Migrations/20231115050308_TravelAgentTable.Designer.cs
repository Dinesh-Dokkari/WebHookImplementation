﻿// <auto-generated />
using AirLineWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AirLineWeb.Migrations
{
    [DbContext(typeof(AirlineDbContext))]
    [Migration("20231115050308_TravelAgentTable")]
    partial class TravelAgentTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AirLineWeb.Models.FlightDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FlightCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(6,2)");

                    b.HasKey("Id");

                    b.ToTable("FlightDetails");
                });

            modelBuilder.Entity("AirLineWeb.Models.WebHookSubscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Secrect")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebHookPublisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebHookType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebHookURi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WebHookSubscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
