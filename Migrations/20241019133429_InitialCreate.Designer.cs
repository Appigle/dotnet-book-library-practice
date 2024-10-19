﻿// <auto-generated />
using LeiChenMidTermTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LeiChenMidTermTest.Migrations
{
    [DbContext(typeof(MidTerm8945274Context))]
    [Migration("20241019133429_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("MidTest.Models.Book", b =>
                {
                    b.Property<int>("PubID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PubID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("MidTest.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Autobiography"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Biography"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Business"
                        },
                        new
                        {
                            ID = 4,
                            Name = "History"
                        },
                        new
                        {
                            ID = 5,
                            Name = "Politics"
                        },
                        new
                        {
                            ID = 6,
                            Name = "Science"
                        },
                        new
                        {
                            ID = 7,
                            Name = "War"
                        },
                        new
                        {
                            ID = 8,
                            Name = "Adventure"
                        },
                        new
                        {
                            ID = 9,
                            Name = "Classics"
                        },
                        new
                        {
                            ID = 10,
                            Name = "Mystery"
                        },
                        new
                        {
                            ID = 11,
                            Name = "Novel"
                        },
                        new
                        {
                            ID = 12,
                            Name = "Poetry"
                        },
                        new
                        {
                            ID = 13,
                            Name = "Plays"
                        },
                        new
                        {
                            ID = 14,
                            Name = "Romance"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
