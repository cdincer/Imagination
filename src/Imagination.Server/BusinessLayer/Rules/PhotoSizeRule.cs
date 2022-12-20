using Imagination.Entities;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Imagination.BusinessLayer.Rules
{
    public class PhotoSizeRule : IPhotoCheckRule
    {
        public bool CheckPhotoRule(byte[] FilePiece, int FileSize)
        {
            bool result = true;
            try
            {
                ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;
                List<ConfigEntity> FileRulesFile = cache["FileRules"] as List<ConfigEntity>;
                string FileSizeMin = FileRulesFile.FirstOrDefault(s => s.Name == "MinFileSize").Value;
                string FileSizeMax = FileRulesFile.FirstOrDefault(s => s.Name == "MaxFileSize").Value;

             
                if(FileSize <= int.Parse(FileSizeMin))
                {
                    return false;
                }
                else if (FileSize > int.Parse(FileSizeMax))
                {
                    return false;
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Photo Type Rule Failed");
                return false;
            }
        }
    }
}
