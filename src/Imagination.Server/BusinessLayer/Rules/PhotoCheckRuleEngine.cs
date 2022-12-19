using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imagination.BusinessLayer.Rules
{
    public class PhotoCheckRuleEngine
    {
        List<IPhotoCheckRule> _rules = new List<IPhotoCheckRule>();
        public PhotoCheckRuleEngine(IEnumerable<IPhotoCheckRule> rules)
        {
            _rules.AddRange(rules);
        }

        public bool PhotoCheckRules(byte[] FilePiece, int FileSize)
        {
            bool decision = false;
            foreach(var rule in _rules)
            {
                decision = rule.CheckPhotoRule(FilePiece,FileSize);
                if (!decision)
                    break;
            }
            return decision;
        }
    }
}
