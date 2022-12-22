using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Drawing;
using System.Collections.Specialized;
using System.Text;
using Imagination.DataLayer.UploadService;
using Imagination.DataLayer;
using Newtonsoft.Json.Linq;
using Imagination.BusinessLayer.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Imagination.BusinessLayer
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertController : ControllerBase
    {
        private readonly ILogger<ConvertController> _logger;
        private readonly ImaginationContext _context;
        private readonly IUploadServiceRepository _UploadServiceRepo;

        public ConvertController(ImaginationContext context,ILogger<ConvertController> logger, IUploadServiceRepository UploadServiceRepo)
        {
            _logger = logger;
            _UploadServiceRepo = UploadServiceRepo;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> TakeAsync(CancellationToken cancellationToken)
        {

            var myRequest = Request;
            Stream requestBody = myRequest.Body;
            byte[] items;
            string FileName = Guid.NewGuid().ToString();
            byte[] spareFile = new byte[3];
            string SavedPath = "";
            

            using (var memoryStream = new MemoryStream())
            {
                await requestBody.CopyToAsync(memoryStream);
                items = memoryStream.ToArray();
            }

            if(items.Length == 0)
            {
                throw new ApplicationException("Please check your file size or format");
            }

            //Make it go through our rules filter.
            Array.Copy(items,0,spareFile,0, 3);
            PhotoChecker photoChecker = new PhotoChecker();
            bool Judgement = photoChecker.PhotoCheckProcess(spareFile, items.Length);

          
            if (Judgement)
            {
                SavedPath = _UploadServiceRepo.AddUploadEntity(items);
            }
            else
            {
                throw new ApplicationException("Please check your file size or format");

            }
            Byte[] b;
            b = await System.IO.File.ReadAllBytesAsync(SavedPath);
            return File(b, "image/jpeg");
        }
    }
}
