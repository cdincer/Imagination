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

        public ImaginationContext(DbContextOptions<ImaginationContext> options) : base(options)
        {
            var ConfigData = System.IO.File.ReadAllText("Configuration/Config.json");
            var ConfigRules = JsonConvert.DeserializeObject<List<ConfigEntity>>(ConfigData);
            ConfigEntity DBFileName = ConfigRules.FirstOrDefault(x => x.Name == "DBFileName");
            DbPath = System.IO.Path.Join(DBFileName.Value);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<UploadEntity> UploadEntity { get; set; }
    }
}
