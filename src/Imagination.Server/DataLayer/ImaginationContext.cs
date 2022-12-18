using Imagination.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Imagination.DataLayer
{
    public class ImaginationContext :DbContext
    {
        public string DbPath { get; }

        public ImaginationContext()
        {
            //For the time being hardcoded file path.
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            string path = Directory.GetCurrentDirectory() + "\\DB\\";
            DbPath = System.IO.Path.Join(path, "blogging.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<UploadEntity> UploadEntity { get; set; }
    }
}
