using Imagination.DataLayer;
using Imagination.Entities;
using System.IO;
using System;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Serilog.Sinks.File;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Runtime.Caching;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace Imagination.DataLayer.UploadService
{
    public class UploadServiceRepository : IUploadServiceRepository
    {
        private readonly ImaginationContext _context;

        public UploadServiceRepository(ImaginationContext context)
        {
            _context = context;
        }


       string MakeImage(byte[] items)
        {
            string path = "";
            try
            {
                string FileName = Guid.NewGuid().ToString();
                int FileSize = items.Length;
                ProcessBeginLog(FileSize, FileName);
                ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;
                List<ConfigEntity> ConfigRules = cache["ConfigRules"] as List<ConfigEntity>;
                ConfigEntity FileSavingDetails = ConfigRules.FirstOrDefault(x => x.Name == "ProcessedFiles");
                ConfigEntity FileSavingExtension = ConfigRules.FirstOrDefault(x => x.Name == "FileExtension");
                path = Directory.GetCurrentDirectory()+ FileSavingDetails.Value + FileName + FileSavingExtension.Value;            
                using (FileStream fs = File.Create(path))
                {
                    // Add some information to the file.
                    fs.Write(items, 0, items.Length);
                }
                ProcessEndLog(FileSize, FileName);      
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "MakeImage Failed");
            }
            return path;
        }

        public  string AddUploadEntity(byte[] items)
        {
            string mySavedFile= MakeImage(items);
            return mySavedFile;
        }


        public async Task<UploadEntity> ProcessBeginLog(int FileSize, string FileName)
        {
            UploadEntity BeginProcess = new UploadEntity();
            BeginProcess.Status = "Begin";
            BeginProcess.FileSize = FileSize;
            BeginProcess.FileName = FileName;
            BeginProcess.UploadBeginDate = DateTime.Now;
            _context.UploadEntity.Add(BeginProcess);
            await _context.SaveChangesAsync();
            return BeginProcess;
        }

        public async Task<UploadEntity> ProcessEndLog(int FileSize, string FileName)
        {
            UploadEntity EndProcess = await _context.UploadEntity.FirstOrDefaultAsync(s => s.FileSize == FileSize && s.FileName == FileName);
            EndProcess.Status = "Completed";
            EndProcess.UploadEndDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return EndProcess;
        }

       
    }
}
