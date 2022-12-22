using Imagination.Entities;
using System.IO;
using System.Threading.Tasks;

namespace Imagination.DataLayer.UploadService
{
    public interface IUploadServiceRepository
    {
       string AddUploadEntity(byte[] items);
    }
}
