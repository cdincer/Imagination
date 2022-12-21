﻿// <auto-generated />
using System;
using Imagination.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Imagination.Migrations
{
    [DbContext(typeof(ImaginationContext))]
    [Migration("20221221175605_ColumnOrder")]
    partial class ColumnOrder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("Imagination.Entities.UploadEntity", b =>
                {
                    b.Property<string>("FileName")
                        .HasColumnType("TEXT")
                        .HasColumnOrder(0);

                    b.Property<int>("FileSize")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(1);

                    b.Property<string>("Status")
                        .HasColumnType("TEXT")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("UploadBeginDate")
                        .HasColumnType("TEXT")
                        .HasColumnOrder(3);

                    b.Property<DateTime>("UploadEndDate")
                        .HasColumnType("TEXT")
                        .HasColumnOrder(4);

                    b.HasKey("FileName");

                    b.ToTable("UploadEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
