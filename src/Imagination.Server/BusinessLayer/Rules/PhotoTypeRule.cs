using Imagination.Entities;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Imagination.BusinessLayer.Rules
{
    public class PhotoTypeRule : IPhotoCheckRule
    {
        public bool CheckPhotoRule(byte[] FilePiece, int FileSize)
        {
            bool result = false;
            try
            {       
                ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;
                List<ConfigEntity> FileExtensions = cache["FileRules"] as List<ConfigEntity>;


                foreach (ConfigEntity configEntity in FileExtensions)
                {
                    string[] ExtensionsChars = configEntity.Value.Split(',');
                    byte[] ExtensionArray = new byte[ExtensionsChars.Length];
                    for(int i=0; i< ExtensionArray.Length;i++)
                    {
                        ExtensionArray[i] = byte.Parse(ExtensionsChars[i]);                  
                    }
                    result = FilePiece.Take(3).SequenceEqual(ExtensionArray);
                    if (result)
                        break;
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
