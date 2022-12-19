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
        private static readonly byte[] bytes = { 255, 216, 255 };//jpg format checker.caching file candidate
        private static readonly byte[] bytes2 = { 137, 80, 78 };//png format checker.caching file candidate
        public bool CheckPhotoRule(byte[] FilePiece, int FileSize)
        {
            bool result = false;
            try
            {
                var FileRulesFile = System.IO.File.ReadAllText("Configuration/FileRules.json");
                var FileRulesList = JsonConvert.DeserializeObject<List<ConfigEntity>>(FileRulesFile);
                List<ConfigEntity> FileExtensions = FileRulesList.ToList();

                foreach(ConfigEntity configEntity in FileExtensions)
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
