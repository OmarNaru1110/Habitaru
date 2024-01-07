﻿// <auto-generated />
using System;
using Habitaru.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Habitaru.Migrations
{
    [DbContext(typeof(HabitContext))]
    partial class HabitContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Habitaru.Models.Habit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvgStreakPeriod")
                        .HasColumnType("int");

                    b.Property<DateTime>("CurStreakDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FirstStreakDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxStreakPeriod")
                        .HasColumnType("int");

                    b.Property<int>("MinStreakPeriod")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrevStreakPeriod")
                        .HasColumnType("int");

                    b.Property<int>("ResetCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Habits");
                });
#pragma warning restore 612, 618
        }
    }
}
