using Imagination.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Caching;

namespace Imagination.DataLayer
{
    public class ImaginationContext :DbContext
    {
        public string DbPath { get; }

        public ImaginationContext()
        {
            ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;
            string fileContents = cache["ConfigRules"] as string;
            var ConfigRules = JsonConvert.DeserializeObject<List<ConfigEntity>>(fileContents);
            ConfigEntity DBFileLocation = ConfigRules.FirstOrDefault(x => x.Name == "DBFileLocation");
            ConfigEntity DBFileName = ConfigRules.FirstOrDefault(x => x.Name == "DBFileName");
            string path = Directory.GetCurrentDirectory() + DBFileLocation.Value;
            DbPath = System.IO.Path.Join(path, DBFileName.Value);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<UploadEntity> UploadEntity { get; set; }
    }
}
