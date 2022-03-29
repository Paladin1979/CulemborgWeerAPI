﻿// <auto-generated />
using System;
using CulemborgWeerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CulemborgWeerAPI.Migrations
{
    [DbContext(typeof(CulemborgWeerContext))]
    [Migration("20220328180624_InitialDb")]
    partial class InitialDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("CulemborgWeerAPI.Data.Entities.WeatherInformation", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double>("Clouds")
                        .HasColumnType("REAL");

                    b.Property<double>("HumidityPercentage")
                        .HasColumnType("REAL");

                    b.Property<double>("Temperature")
                        .HasColumnType("REAL");

                    b.Property<double>("WindDirection")
                        .HasColumnType("REAL");

                    b.Property<double>("WindSpeed")
                        .HasColumnType("REAL");

                    b.HasKey("Date");

                    b.ToTable("WeatherInformation");
                });
#pragma warning restore 612, 618
        }
    }
}
