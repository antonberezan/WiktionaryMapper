using System.Collections.Generic;

namespace Memoling.Tools.WiktionaryParser
{
    public class Quote
    {
        public string Author { get; set; }

        public ICollection<string> Quotes { get; set; } 
    }
}