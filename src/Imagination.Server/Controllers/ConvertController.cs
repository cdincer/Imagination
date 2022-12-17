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

namespace Imagination.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ConvertController> _logger;

        public ConvertController(ILogger<ConvertController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task TakeAsync( CancellationToken cancellationToken)
        {
            var myRequest = Request;
            Stream requestBody = myRequest.Body;
            //Goal is stream to byte array
            //string path = @"C:\Users\Can\Documents\Visual Studio Code\Imagination\resources2\"+Guid.NewGuid()+".jpg";
            byte[] items;
            using (var memoryStream = new MemoryStream())
            {
                await requestBody.CopyToAsync(memoryStream);
                items = memoryStream.ToArray();
            }


            string path = @"C:\Users\Can\Documents\Visual Studio Code\Imagination\resources2\"+Guid.NewGuid()+".jpg";
            using (FileStream fs = System.IO.File.Create(path))
            {
                // Add some information to the file.
                fs.Write(items, 0, items.Length);
            }

            Console.WriteLine("aaa");

            Console.WriteLine("bbb");
        }
    }
}
