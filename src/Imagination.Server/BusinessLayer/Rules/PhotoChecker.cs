using Serilog;
using System.Collections.Generic;
using System;

namespace Imagination.BusinessLayer.Rules
{
    public class PhotoChecker
    {

        public bool PhotoCheckProcess(byte[] FilePiece, int FileSize)
        {
            try
            {
                var rules = new List<IPhotoCheckRule>();
                rules.Add(new PhotoTypeRule());
                rules.Add(new PhotoSizeRule());

                var engine = new PhotoCheckRuleEngine(rules);
                return engine.PhotoCheckRules(FilePiece, FileSize);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Rule Checking Failed");
                return false;
            }
        }
    }
}
