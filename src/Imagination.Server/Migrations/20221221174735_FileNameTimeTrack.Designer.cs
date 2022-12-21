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
    [Migration("20221221174735_FileNameTimeTrack")]
    partial class FileNameTimeTrack
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("Imagination.Entities.UploadEntity", b =>
                {
                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.Property<int>("FileSize")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UploadBeginDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UploadEndDate")
                        .HasColumnType("TEXT");

                    b.HasKey("FileName");

                    b.ToTable("UploadEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
