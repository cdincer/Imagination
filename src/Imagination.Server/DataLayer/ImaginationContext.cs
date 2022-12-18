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
            //I wanted to make a easily editable way of creating and placing files.
            //These things normally wouldn't be read from a json file. I would take them from a cached file.
            //Because of  dotnet ef database update behaviour I was forced to take them from a json file every time.
            var ConfigData = System.IO.File.ReadAllText("Configuration/Config.json");
            var ConfigRules = JsonConvert.DeserializeObject<List<ConfigEntity>>(ConfigData);
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
