using Imagination.DataLayer;
using Imagination.Entities;
using System.IO;
using System;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Serilog.Sinks.File;

namespace Imagination.DataLayer.UploadService
{
    public class UploadServiceRepository : IUploadServiceRepository
    {
        private readonly ImaginationContext _context;

        public UploadServiceRepository(ImaginationContext context)
        {
            _context = context;
        }


        public async Task MakeImage(byte[] items)
        {
            try
            {
                string FileName = Guid.NewGuid().ToString();
                int FileSize = items.Length;
                ProcessBeginLog(FileSize, FileName);
                //TO:DO REMOVE MAGIC STRINGS
                string path = Directory.GetCurrentDirectory()+"\\ProcessedFiles\\" + FileName + ".jpeg";
                using (FileStream fs = File.Create(path))
                {
                    // Add some information to the file.
                    fs.Write(items, 0, items.Length);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "MakeImage Failed");
            }
        }

        public async Task AddUploadEntity(byte[] items)
        {
            MakeImage(items);
        }


        public async Task<UploadEntity> ProcessBeginLog(int FileSize, string FileName)
        {
            UploadEntity BeginProcess = new UploadEntity();
            BeginProcess.Status = "Begin";
            BeginProcess.FileSize = FileSize;
            BeginProcess.FileName = FileName;
            BeginProcess.UploadDate = DateTime.Now;
            _context.UploadEntity.Add(BeginProcess);
            await _context.SaveChangesAsync();
            return BeginProcess;
        }
    }
}
